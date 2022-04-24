using System;
using System.Collections.Generic;
using System.IO;

namespace metaPost
{
    class Program
    {
        static void Main(string[] args)
        {

            string ruta_pdf_in = @"~\Documents\pdfs\doc_in.pdf";//Original pdf path
            string ruta_pdf_temp = @"~\Documents\pdfs\temp.pdf";//pdf out with metadata
            string ruta_pdf_final = @"~\Documents\pdfs\pdfAmeta.pdf";//pdfa out with metadata

            AddMetadata(ruta_pdf_in, ruta_pdf_temp);
           
            PdfaConverter(ruta_pdf_temp,ruta_pdf_final);
            
        }
        static void  AddMetadata(string input, string output)
        {
            
            string inputFile = input;
            string outputFile = output;

            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            using (System.IO.FileStream fs = new System.IO.FileStream(outputFile, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                using (iTextSharp.text.pdf.PdfStamper stamper = new iTextSharp.text.pdf.PdfStamper(reader, fs))
                {
                    Dictionary<String, String> info = reader.Info;
                    
                    info.Add("Keywords", "Metadata value");
                    

                    stamper.MoreInfo = info;

                    stamper.Close();
                    
                } 
            }


        }
        static void PdfaConverter(string inn, string outt)
        {
            //parametros de entrada y salida de los pdf
            string pinput = inn;
            string poutput = outt;
            //path del .bat
            string path = @"~\batch.bat";
            using (StreamWriter file = new StreamWriter(path))
            {
                //contenido del .bat
                file.WriteLine(@"cd \");
                file.WriteLine(@"cd Program Files");
                file.WriteLine(@"cd PDF2PDFA-CL");
                file.WriteLine($@"PDF2PDFA-CL.exe /src=""{pinput}"" /dst=""{poutput}"""); 
            }
           
            //ejecucion del .bat
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = @"~\batch.bat";
            process.StartInfo = startInfo;
            process.Start();
            
        }

    }
}
