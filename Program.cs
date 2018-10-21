using Arithmetic_Obfuscation.Arithmetic;
using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Arithmetic_Obfuscation
{
    static class Program
    {

        static void Main(string[] args)
        {
            Logger logger = new Logger();
            if (args.Length < 1)
            {
                logger.Error("Please Drag and Drop the file you want to obfuscate it.");
            }
            ModuleDef module = ModuleDefMD.Load(args[0]);
            logger.mark();
            logger.Info("File loaded successfully.");
            new Arithmetic.Arithmetic(module).Execute();
            string newPath = Path.GetDirectoryName(module.Location) + "\\" + Path.GetFileNameWithoutExtension(module.Location) + "-Obfuscated" + Path.GetExtension(module.Location);
            logger.Info("new Path: " + newPath);
            logger.Success("Writing...");
            try
            {
                module.Write(newPath);
                logger.Success("Saved Successfully.");
            }
            catch (Exception e)
            {
                logger.Info("There's an error at writing, Don't worry we will try to re-write it again.");
                module.Write(newPath, new dnlib.DotNet.Writer.ModuleWriterOptions(module) { Logger = DummyLogger.NoThrowInstance });
                logger.Success("Saved Successfully.");
            }
            logger.Info("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
