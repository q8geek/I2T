using System;
using System.Drawing;
using System.IO;

namespace I2T
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0)
                PrintTool();
            else if (args.Length == 1)
            {
                if (CheckExtension(args[0]))
                    ConvertToTable(args[0]);
                else
                    Console.WriteLine("ERROR: File extension is not supported.");
            }
            else
                Console.WriteLine("ERROR: Wrong parameters.");

        }

        static bool CheckExtension(string fileName)
        {
            string[] chops = fileName.Split('.');
            bool blReturn = false;
            if (chops.Length > 1)
            {
                if (chops[chops.Length - 1].ToLower() == "jpg")
                    blReturn = true;
                else if (chops[chops.Length - 1].ToLower() == "png")
                    blReturn = true;
                else if (chops[chops.Length - 1].ToLower() == "bmp")
                    blReturn = true;
            }

            return blReturn;
        }

        static void ConvertToTable(string fileName)
        {
            try
            {
                string strNewName = fileName + ".html";
                Image imgTemp = Image.FromFile(fileName);
                Bitmap imgBM = new Bitmap(imgTemp);
                string strHTML = "<table cellspacing=\"0\">";

                int width = imgBM.Width;
                int height = imgBM.Height;

                for (int y = 0; y < height; y++)
                {
                    string strRow = "<tr>";
                    for (int x = 0; x < width; x++)
                    {
                        string strCell = "<td bgcolor=\"#";
                        Color tempColor = imgBM.GetPixel(x, y);
                        strCell += tempColor.R.ToString("X2") + tempColor.G.ToString("X2") + tempColor.B.ToString("X2");
                        strCell += "\"></td>";
                        strRow += strCell;
                    }
                    strRow += "</tr>";
                    strHTML += strRow;
                }
                strHTML += "</table>";
                System.IO.File.WriteAllText(strNewName, strHTML);
                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION ERROR: " + ex.Message);
            }
        }

        static void PrintTool()
        {
            Console.WriteLine("This tool converts images to HTML tables.");
            Console.WriteLine("Usage: i2t FILE");
            Console.WriteLine("");
            Console.WriteLine("Where \"FILE\" is a JPG, PNG, or BMP image.");
        }

    }
}
