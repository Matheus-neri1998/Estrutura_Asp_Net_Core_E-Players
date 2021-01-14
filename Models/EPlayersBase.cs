using System.Collections.Generic;
using System.IO;

namespace E_Players_Asp_Net_Core.Models
{
    public class EPlayersBase
    {
        public void CreateFolderAndFile(string path)
        {   
            // Database/Equipe.csv
            string folder = path.Split("/")[0];

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        } // fim de CreateFolderAndFile


        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> lines = new List<string>();

            // using -> abrir e fechar determinado tipo de arquivo ou conexão
            // StreamReader -> Ler as informações do meu csv
            using (StreamReader file = new StreamReader(path))
            {
                string linha;
                while ((linha = file.ReadLine()) != null)
                {
                    lines.Add(linha);
                }
            } // fim de StreamReader


            return lines;

        } // fim de ReadAllLines

        public void ReWriteCSV(string path, List<string> lines)
        {
            // StreamWriter -> Escrita de arquivos
            using (StreamWriter output = new StreamWriter(path))
            {
                foreach (var item in lines)
                {
                    output.Write(item + "\n");
                }
            }
        } // fim de ReWriteCSV
    }
}