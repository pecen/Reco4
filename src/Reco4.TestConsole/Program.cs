using Microsoft.Win32;
using Reco4.Library;
using Reco4.Library.Enum;
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
    private static int _errors;

    public static VehiclesInfo Vehicles { get; set; }
    public static ComponentList Components { get; set; }
    public static HashSet<string> PDNumbers { get; set; }

    [STAThread]
    static void Main(string[] args) {
      while (true) {
        try {
          ShowMenu();

          var input = ReadLine();

          switch (input) { //ReadKey().KeyChar) {
            case "1": GetRoadmapGroups(); break;
            case "2": GetRoadmapGroup(); break;
            case "3": SearchRoadmapGroups(); break;
            case "4": CreateRoadmapGroup(); break;
            case "5": UpdateRoadmapGroup(); break;
            case "6": DeleteRoadmapGroup(); break;
            case "7": GetVehicles(); break;
            case "8": CheckComponentsForVehicles(); break;
            case "9": CreateRoadmapGroupWRules(); break;
            case "10": LockAndCreate(); break;
            case "11": ConvertVehicles(); break;
            case "12": Simulate(); break;
            case "99": SplitString(); break;
            case "0": WriteLine(); return;

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

    private static void SplitString() {
      WriteLine("Hit <Enter> to choose file with string(s) to read.");
      ReadLine();
      WriteLine($"Number of strings = {HandleItemLine(GetContent(GetFileDialog(FileTypes.pub.ToString())))}");
    }

    private static int HandleItemLine(string line) {
      return line
        .Split(new char[] { ' ', '\r', '\n' })
        .Where(c => !string.IsNullOrEmpty(c))
        .ToArray()
        .Count();
    }

    private static void Simulate() {
      throw new NotImplementedException();
    }

    private static void ConvertVehicles() {
      throw new NotImplementedException();
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
      WriteLine(" 9) Create a new Roadmap Group via Rules settings");
      WriteLine(" 10) Lock and Create Roadmap");
      WriteLine(" 11) Convert Vehicles");
      WriteLine(" 12) Simulate");
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
      _errors = 0;

      WriteLine("\nEnter a name for the Roadmap");
      var name = ReadLine();
      WriteLine("Enter start year: ");
      var startYear = int.Parse(ReadLine());
      WriteLine("Enter end year: ");
      var endYear = int.Parse(ReadLine());

      var xmlStream = GetFileDialog(FileTypes.xml.ToString());

      if (xmlStream == null) {
        WriteLine("Action was cancelled");

        return;
      }

      Vehicles = VehiclesInfo.GetVehicles(xmlStream);

      CheckComponentsForVehicles();

      var roadmap = RoadmapGroupEdit.CreateRoadmapGroup();

      roadmap.RoadmapName = name;
      roadmap.CreationTime = DateTime.Now;
      roadmap.StartYear = startYear;
      roadmap.EndYear = endYear;
      roadmap.ConvertToVehicleInputStatusValue = ConvertToVehicleInputStatus.Pending;

      //string msg = string.Empty;

      //if (_errors != 0) {
      //  msg = $"Missing Components in database. Found {_errors} errors in Xml-file.";
      //  msg += "\nRoadmap Group created but no Xml uploaded";
      //  roadmap.ValidationStatusValue = ValidationStatus.ValidatedWithFailures;
      //}
      //else {
      //  roadmap.Xml = GetXml(xmlStream);
      //  roadmap.ValidationStatusValue = ValidationStatus.ValidatedWithSuccess;
      //  msg = "Roadmap created successfully!!";
      //}

      var msg = CheckForErrors(xmlStream, roadmap);

      roadmap = roadmap.Save();

      WriteLine(msg);
    }

    private static void UpdateRoadmapGroup() {
      _errors = 0;

      WriteLine("\nEnter the RoadmapGroupId for the RoadmapGroup you want to change: ");
      var id = ReadLine();
      var roadmap = RoadmapGroupEdit.GetRoadmapGroup(int.Parse(id));

      WriteLine($"Current start year is {roadmap.StartYear}");
      WriteLine("Enter a new start year: ");
      var startYear = int.Parse(ReadLine());

      WriteLine($"Current EndYear is {roadmap.EndYear}");
      WriteLine("Enter a new end year: ");
      var endYear = int.Parse(ReadLine());

      var xmlStream = GetFileDialog(FileTypes.xml.ToString());

      if (xmlStream == null) {
        WriteLine("Action was cancelled");

        return;
      }

      Vehicles = VehiclesInfo.GetVehicles(xmlStream);

      CheckComponentsForVehicles();

      roadmap.StartYear = startYear;
      roadmap.EndYear = endYear;

      var msg = CheckForErrors(xmlStream, roadmap);

      roadmap = roadmap.Save();

      WriteLine(msg);
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
      var xmlStream = GetFileDialog(FileTypes.xml.ToString());

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

    private static void CheckComponentsForVehicles() {
      if (Vehicles == null) {
        //WriteLine("There are no Vehicles loaded. Run GetVehicles first.");
        //return;
        GetVehicles();
      }

      WriteLine("\nChecking components...");
      WriteLine($"Fetching Components at time {DateTime.Now}");
      Components = ComponentList.GetComponents();
      WriteLine($"Finished fetching at time {DateTime.Now}");

      PDNumbers = new HashSet<string>(new PDComparer());
      foreach (var item in Components) {
        PDNumbers.Add(item.PDNumber);
      }

      WriteLine($"Start time: {DateTime.Now.ToString("hh:mm:ss.fff")}");

      foreach (var vehicle in Vehicles?.Vehicles.Vehicle) {
        _errors += GetComponentErrors(vehicle.Components.Engine.EnginePD);
        _errors += GetComponentErrors(vehicle.Components.GearBoxPD);
        _errors += GetComponentErrors(vehicle.Components.AxleGearPD);
        _errors += GetComponentErrors(vehicle.Components.RetarderPD);
        _errors += GetComponentErrors(vehicle.Components.TorqueConverterPD);

        foreach (var axle in vehicle.Components.AxleWheels.Data.Axles.Axle) {
          _errors += GetComponentErrors(axle.TyrePD);
        }
      }

      WriteLine($"End time: {DateTime.Now.ToString("hh:mm:ss.fff")}");
      WriteLine($"All the vehicles components checked. Found {_errors} errors");
    }

    private static int GetComponentErrors(string pd) {
      if (!string.IsNullOrEmpty(pd)) {
        return PDNumbers.Contains(pd) ? 0 : 1;
      }

      return 0;
    }

    private static void LockAndCreate() {
      WriteLine("\nEnter the Roadmap Group you want to Lock and Create:");
      var id = ReadLine();

      var roadmap = RoadmapGroupEdit.GetRoadmapGroup(int.Parse(id));

      if (roadmap.ValidationStatusValue != ValidationStatus.ValidatedWithSuccess) {
        WriteLine("Failed! The Roadmap Group must be validated with success before you can do a Lock and Create.");
        return;
      }

      roadmap.ConvertToVehicleInputStatusValue = ConvertToVehicleInputStatus.Processing;
      roadmap = roadmap.Save();

      WriteLine("Roadmap locked and created with success!");
    }

    private static void CreateRoadmapGroupWRules() {
      var roadmap = RoadmapGroupEdit.CreateRoadmapGroup();

      WriteLine("\nEnter a name for the Roadmap");
      roadmap.RoadmapName = ReadLine();
      WriteLine("Enter start year: ");
      var s = ReadLine();
      roadmap.StartYear = string.IsNullOrEmpty((s)) ? 0 : int.Parse(s);
      WriteLine("Enter end year: ");
      s = ReadLine();
      roadmap.EndYear = string.IsNullOrEmpty(s) ? 0 : int.Parse(s);

      if (roadmap.BrokenRulesCollection.Count() > 0) {
        foreach (var error in roadmap.BrokenRulesCollection) {
          WriteLine(error.Description);
        }

        return;
      }

      var xmlStream = GetFileDialog(FileTypes.xml.ToString());

      if (xmlStream == null) {
        WriteLine("Action was cancelled");

        return;
      }

      roadmap.CreationTime = DateTime.Now;
      WriteLine($"Checking components...");
      roadmap.Xml = GetContent(xmlStream);
      roadmap.ConvertToVehicleInputStatusValue = ConvertToVehicleInputStatus.Pending;

      if (roadmap.IsSavable) {
        WriteLine($"Start saving at: {DateTime.Now.ToString("hh:mm:ss.fff")}");
        roadmap = roadmap.Save();
        WriteLine($"End saving at: {DateTime.Now.ToString("hh:mm:ss.fff")}");
        if (roadmap.ValidationStatusValue == ValidationStatus.ValidatedWithSuccess) {
          WriteLine("Roadmap Group successfully saved!");
        }
        else {
          foreach (var item in roadmap.BrokenRulesCollection) {
            WriteLine(item.Description);
          }
        }
      }
      else {
        foreach (var error in roadmap.BrokenRulesCollection) {
          WriteLine(error.Description);
        }
      }
    }

    private static Stream GetFileDialog(string suffix) {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog {
        //InitialDirectory = Environment.CurrentDirectory,
        Filter = $"{char.ToUpper(suffix[0]) + suffix.Substring(1)} files (*.{suffix})|*.{suffix}|All files (*.*)|*.*",
        FilterIndex = 1,
        RestoreDirectory = true
      };

      if ((bool)openFileDialog.ShowDialog()) {
        //Read the contents of the file into a stream
        return openFileDialog.OpenFile();
      }

      return null;
    }

    private static string GetContent(Stream stream) {
      stream.Position = 0;

      using (StreamReader reader = new StreamReader(stream)) {
        return reader.ReadToEnd();
      }
    }

    private static string CheckForErrors(Stream xmlStream, RoadmapGroupEdit roadmap) {
      string msg = string.Empty;

      if (_errors != 0) {
        msg = $"Missing Components in database. Found {_errors} errors in Xml-file.";
        msg += "\nRoadmap Group created but no Xml uploaded";
        roadmap.ValidationStatusValue = ValidationStatus.ValidatedWithFailures;
      }
      else {
        roadmap.Xml = GetContent(xmlStream);
        roadmap.ValidationStatusValue = ValidationStatus.ValidatedWithSuccess;
        msg = "Roadmap created/updated successfully!!";
      }

      return msg;
    }
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
    }
  }
}
