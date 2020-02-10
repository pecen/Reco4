using Microsoft.Win32;
using Reco4.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Console;

namespace Reco4.TestConsole {
  class Program {
    public static VehiclesInfo Vehicles { get; set; }
    public static ComponentList Components { get; set; }
    public static HashSet<string> PDNumbers { get; set; }

    [STAThread]
    static void Main(string[] args) {
      while (true) {
        try {
          ShowMenu();

          switch (ReadKey().KeyChar) {
            case '1': GetRoadmapGroups(); break;
            case '2': GetRoadmapGroup(); break;
            case '3': SearchRoadmapGroups(); break;
            case '4': CreateRoadmapGroup(); break;
            case '5': UpdateRoadmapGroup(); break;
            case '6': DeleteRoadmapGroup(); break;
            case '7': GetVehicles(); break;
            case '8': CheckComponentsForVehicle(); break;
            case '0': WriteLine(); return;

            default: ShowMenu(); break;
          }
        }
        catch (Exception ex) {
          WriteLine();
          WriteLine("There was an error: ");
          while (ex != null) {
            WriteLine(ex.Message);
            ex = ex.InnerException;
          }
        }

        WriteLine();
        WriteLine("Press <ENTER> to return to menu.");
        ReadLine();
      }
    }

    public static void ShowMenu() {
      Clear();
      WriteLine("PREREQ:");
      WriteLine("1. Set first req here,");
      WriteLine("2. Set second req here");
      WriteLine("");
      WriteLine("- Select DecryptionService command.");
      WriteLine("");
      WriteLine(" 1) Get Roadmap Groups.");
      WriteLine(" 2) Get specific Roadmap Group.");
      WriteLine(" 3) Search Roadmap Groups on Name.");
      WriteLine(" 4) Create a new Roadmap Group");
      WriteLine(" 5) Update a specific Roadmap Group");
      WriteLine(" 6) Delete a specific Roadmap Group");
      WriteLine(" 7) Get all vehicles");
      WriteLine(" 8) Check the vehicles components");
      WriteLine(" 0) Exit");

      WriteLine("");
      Write(" > ");
    }

    private static void GetRoadmapGroup() {
      WriteLine("\nEnter a Roadmap Group Id: ");
      var id = int.Parse(ReadLine());

      var item = RoadmapGroupEdit.GetRoadmapGroup(id);

      if (item.RoadmapGroupId == 0) {
        WriteLine($"No Roadmap Group with the id {id} in the database");
      }
      else {
        WriteLine($"Roadmap Group {item.RoadmapGroupId}: {item.RoadmapName}");
      }
    }

    private static void SearchRoadmapGroups() {
      WriteLine("\nEnter a search criteria: ");
      var filter = ReadLine();

      WriteLine(Environment.NewLine);
      WriteLine($"Start time: {DateTime.Now}");

      var list = RoadmapGroupList.GetRoadmapGroups(filter);

      //int i = 0;

      if (list.Count() > 0) {
        foreach (var item in list) {
          WriteLine($"Roadmap Group {item.RoadmapGroupId}: {item.RoadmapName}");
        }
      }
      else {
        WriteLine($"No Roadmap Groups with the given search criteria '{filter}'");
      }

      WriteLine($"End time: {DateTime.Now}");
    }

    private static void GetRoadmapGroups() {
      WriteLine(Environment.NewLine);
      WriteLine($"Start time: {DateTime.Now}");

      var list = RoadmapGroupList.GetRoadmapGroups();

      if (list.Count() > 0) {
        foreach (var item in list) {
          WriteLine($"Roadmap Group {item.RoadmapGroupId}: {item.RoadmapName}");
        }
        WriteLine($"A total of {list.Count()} Roadmap Groups collected.");
      }
      else {
        WriteLine("No Roadmap Groups available");
      }

      WriteLine($"End time: {DateTime.Now}");
    }

    private static void CreateRoadmapGroup() {
      WriteLine("\nEnter a name for the Roadmap");
      var name = ReadLine();
      WriteLine("Enter start year: ");
      var startYear = int.Parse(ReadLine());
      WriteLine("Enter end year: ");
      var endYear = int.Parse(ReadLine());

      var xmlStream = GetFileDialog();

      if (xmlStream == null) {
        WriteLine("Action was cancelled");

        return;
      }

      var roadmap = RoadmapGroupEdit.CreateRoadmapGroup();

      roadmap.RoadmapName = name;
      roadmap.CreationTime = DateTime.Now;
      roadmap.StartYear = startYear;
      roadmap.EndYear = endYear;
      roadmap.Xml = GetXml(xmlStream);

      roadmap = roadmap.Save();

      WriteLine("Roadmap created successfully!!");
    }

    private static void UpdateRoadmapGroup() {
      WriteLine("\nEnter the RoadmapGroupId for the RoadmapGroup you want to change: ");
      var id = ReadLine();
      var roadmap = RoadmapGroupEdit.GetRoadmapGroup(int.Parse(id));

      WriteLine($"Current start year is {roadmap.StartYear}");
      WriteLine("Enter a new start year: ");
      var startYear = int.Parse(ReadLine());

      WriteLine($"Current EndYear is {roadmap.EndYear}");
      WriteLine("Enter a new end year: ");
      var endYear = int.Parse(ReadLine());

      var xmlStream = GetFileDialog();

      if (xmlStream == null) {
        WriteLine("Action was cancelled");

        return;
      }

      roadmap.StartYear = startYear;
      roadmap.EndYear = endYear;
      roadmap.Xml = GetXml(xmlStream);

      roadmap = roadmap.Save();

      WriteLine("Roadmap updated successfully!!");
    }

    private static void DeleteRoadmapGroup() {
      WriteLine("\nEnter the RoadmapGroupId for the RoadmapGroup you want to delete: ");
      var id = ReadLine();
      if (RoadmapGroupEdit.Exists(int.Parse(id))) {
        WriteLine("Hit <Enter> to start deleting.");
      }
      else {
        WriteLine($"The given id ({id}) doesn't exist.");
        return;
      }

      ReadKey();
      RoadmapGroupEdit.DeleteRoadmapGroup(int.Parse(id));

      WriteLine("RoadmapGroup deleted successfully.");
    }

    private static void GetVehicles() {
      WriteLine("\nGetting vehicles...");
      var xmlStream = GetFileDialog();

      if (xmlStream == null) {
        WriteLine("Action was cancelled");
        return;
      }

      WriteLine($"Start time: {DateTime.Now}");
      Cursor.Current = Cursors.WaitCursor;
      Vehicles = VehiclesInfo.GetVehicles(xmlStream);
      Cursor.Current = Cursors.Default;
      WriteLine($"End time: {DateTime.Now}");
      var count = Vehicles.Vehicles.Vehicle.Count();

      WriteLine($"\nSuccessfully fetched {count} vechicle{(count > 1 ? "s" : string.Empty)}");
      WriteLine($"The VIN of the first vehicle is: {Vehicles.Vehicles.Vehicle.FirstOrDefault().VIN}");
    }

    private static void CheckComponentsForVehicle() {
      WriteLine("\nChecking components...");

      int errors = 0;
      //foreach(var vehicle in Vehicles.Vehicles.Vehicle) {
      //  errors += VehiclesInfo.ComponentExists(vehicle.Components.Engine.EnginePD) ? 0 : 1;
      //  errors += VehiclesInfo.ComponentExists(vehicle.Components.GearBoxPD) ? 0 : 1;
      //  errors += VehiclesInfo.ComponentExists(vehicle.Components.AxleGearPD) ? 0 : 1;
      //  errors += VehiclesInfo.ComponentExists(vehicle.Components.RetarderPD) ? 0 : 1;
      //  errors += VehiclesInfo.ComponentExists(vehicle.Components.TorqueConverterPD) ? 0 : 1;

      //  foreach(var axle in vehicle.Components.AxleWheels.Data.Axles.Axle) {
      //    errors += VehiclesInfo.ComponentExists(axle.TyrePD) ? 0 : 1;
      //  }
      //}

      WriteLine($"Fetching Components at time {DateTime.Now}");
      Components = ComponentList.GetComponents();
      WriteLine($"Finished fetching at time {DateTime.Now}");

      PDNumbers = new HashSet<string>(new PDComparer());
      foreach (var item in Components) {
        PDNumbers.Add(item.PDNumber);
      }

      WriteLine($"Start time: {DateTime.Now}");
      //var v = Components.Any(c => c.PDNumber == Vehicles.Vehicles.Vehicle.Any(v => v.Components.Engine.EnginePD);

      if (Vehicles == null) {
        WriteLine("There are no Vehicles loaded. Run GetVehicles first.");
        return;
      }

      foreach (var vehicle in Vehicles?.Vehicles.Vehicle) {
        //errors += Components.Select(c => c.PDNumber).Contains(vehicle.Components.Engine.EnginePD) ? 0 : 1;
        //errors += Components.Select(c => c.PDNumber).Contains(vehicle.Components.GearBoxPD) ? 0 : 1;
        //errors += Components.Select(c => c.PDNumber).Contains(vehicle.Components.AxleGearPD) ? 0 : 1;
        //errors += Components.Select(c => c.PDNumber).Contains(vehicle.Components.RetarderPD) ? 0 : 1;
        //errors += Components.Select(c => c.PDNumber).Contains(vehicle.Components.TorqueConverterPD) ? 0 : 1;

        errors += GetComponentErrors(vehicle.Components.Engine.EnginePD);
        errors += GetComponentErrors(vehicle.Components.GearBoxPD);
        errors += GetComponentErrors(vehicle.Components.AxleGearPD);
        errors += GetComponentErrors(vehicle.Components.RetarderPD);
        errors += GetComponentErrors(vehicle.Components.TorqueConverterPD);

        foreach (var axle in vehicle.Components.AxleWheels.Data.Axles.Axle) {
          //errors += Components.Select(c => c.PDNumber).Contains(axle.TyrePD) ? 0 : 1;
          errors += GetComponentErrors(axle.TyrePD);
        }
      }
      WriteLine($"End time: {DateTime.Now}");

      WriteLine($"All the vehicles components checked. Found {errors} errors");
    }

    private static int GetComponentErrors(string pd) {
      if (!string.IsNullOrEmpty(pd)) {
        //return Components
        //  .Where(c => c.PDNumber == pd)
        //  .Count() == 0
        //  ? 1
        //  : 0;
        
        return PDNumbers.Contains(pd) ? 0 : 1;

        //return Components.Any(c => c.PDNumber == pd)
        //  ? 0
        //  : 1;

        //return Components.Select(c => c.PDNumber)
        //  .Contains(pd)
        //  ? 0
        //  : 1;
      }

      return 0;
    }

    private static Stream GetFileDialog() {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog {
        //InitialDirectory = Environment.CurrentDirectory,
        Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*",
        FilterIndex = 1,
        RestoreDirectory = true
      };

      if ((bool)openFileDialog.ShowDialog()) {
        //Read the contents of the file into a stream
        return openFileDialog.OpenFile();
      }

      return null;
    }

    private static string GetXml(Stream xmlStream) {
      using (StreamReader reader = new StreamReader(xmlStream)) {
        return reader.ReadToEnd();
      }
    }

    //void BtnLoadClick(object sender, EventArgs e) {
    //  using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog()) {
    //    openFileDialog.Filter = "Xml Files (*.xml)|*.xml";
    //    //if (openFileDialog.ShowDialog() == DialogResult.OK) {
    //      XmlReader xmlReader = XmlReader.Create(openFileDialog.FileName);


    //      string strid = "";
    //      TreeNode pName = new TreeNode();
    //      TreeNode ava = new TreeNode();
    //      TreeNode price = new TreeNode();

    //      bool idFlag = false;
    //      bool pNameFlag = false;
    //      bool avaFlag = false;
    //      bool priceFlag = false;

    //      string strName = "";
    //      string strValue = "";

    //      while (xmlReader.Read()) {

    //        switch (xmlReader.NodeType) {
    //          case XmlNodeType.Element:
    //            strName = xmlReader.Name;
    //            switch (strName) {
    //              case "Id":
    //                idFlag = true;
    //                break;
    //              case "Part_Name":
    //                pNameFlag = true;
    //                break;
    //              case "Total_Available":
    //                avaFlag = true;
    //                break;
    //              case "Price":
    //                priceFlag = true;
    //                break;
    //            }
    //            break;
    //          case XmlNodeType.Text:
    //            strValue = xmlReader.Value;

    //            if (idFlag) {
    //              //id = new TreeNode(strValue);
    //              strid = strValue;
    //            }
    //            if (pNameFlag) {
    //              pName = new TreeNode(strValue);
    //            }
    //            if (avaFlag) {
    //              ava = new TreeNode(strValue);
    //            }
    //            if (priceFlag) {
    //              price = new TreeNode(strValue);
    //              TreeNode[] part = new TreeNode[] { pName, ava, price };
    //              TreeNode allPart = new TreeNode(strid, part);
    //              tvData.Nodes.Add(allPart);
    //            }

    //            idFlag = false;
    //            pNameFlag = false;
    //            avaFlag = false;
    //            priceFlag = false;

    //            break;
    //          case XmlNodeType.EndElement:
    //            break;
    //        }
    //      }
    //    }
    //  }
    //}
  }

  public class PDComparer : IEqualityComparer<string> {
    const int _multiplier = 89;

    public bool Equals(string x, string y) {
      return x == y;
    }

    public int GetHashCode(string obj) {
      // Stores the result.
      int result = 0;

      // Don't compute hash code on null object.
      if (obj == null) {
        return 0;
      }

      // Get length.
      int length = obj.Length;

      // Return default code for zero-length strings [valid, nothing to hash with].
      if (length > 0) {
        // Compute hash for strings with length greater than 1
        char let1 = obj[0];          // First char of string we use
        char let2 = obj[length - 1]; // Final char

        // Compute hash code from two characters
        int part1 = let1 + length;
        result = (_multiplier * part1) + let2 + length;
      }
      return result;

      //return obj.GetHashCode();
    }
  }

}
