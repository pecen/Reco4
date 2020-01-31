using Reco4.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Reco4.TestConsole {
  class Program {
    static void Main(string[] args) {
      while (true) {
        try {
          ShowMenu();

          switch (ReadKey().KeyChar) {
            case '1': GetRoadmapGroups(); break;
            case '0': WriteLine(); return;

            default: ShowMenu(); break;
          }
        }
        catch (Exception ex) {
          WriteLine();
          WriteLine("There was an error: " + ex.Message);
          WriteLine();
          WriteLine("Press <ENTER> to return to menu.");
          ReadLine();
        }
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
      WriteLine(" 2) Validate xml against schema.");
      WriteLine(" 3) Save file.");
      WriteLine(" 4) Run DecryptionLambdaService.");
      WriteLine(" 0) Exit");

      WriteLine("");
      Write(" > ");
    }

    private static void GetRoadmapGroups() {
      WriteLine(Environment.NewLine);
      WriteLine($"Start time: {DateTime.Now}");

      var list = RoadmapGroupList.GetRoadmapGroups();

      //int i = 0;

      foreach(var item in list) {
        WriteLine($"Roadmap Group {item.RoadmapGroupId}: {item.RoadmapName}");
      }
      WriteLine($"End time: {DateTime.Now}");

      WriteLine("\nPress key to continue...");
      ReadKey();
    }
  }
}
