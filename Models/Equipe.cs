using System.Collections.Generic;
using System.IO;
using E_Players_Asp_Net_Core.Interfaces;

namespace E_Players_Asp_Net_Core.Models
{
    public class Equipe : EPlayersBase , IEquipe // Chama a superclasse antes da interface que herda por último
    {
        // ID = Identificador único
        
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/Equipe.csv";

        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }

        public string Prepare(Equipe time)
        {
            return $"{time.IdEquipe} {time.Nome} {time.Imagem}";
        }

        public void Create(Equipe Team)
        {
            string [] linhas = { Prepare(Team) };
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
           List<string> linhas = ReadAllLinesCSV(PATH);

            // Removemos a linha que tenha o código a ser alterado
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

            // Reescreve o csv com as alterações
            ReWriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            // Ler todas as linhas do csv
            string [] linhas = File.ReadAllLines(PATH);

            // Percorrer as linhas e adicionar na lista de equipes cada objeto equipe

            foreach (var item in linhas)
            {
                // 1;VivoKeyd;vivo.jpg (Exemplo)
                string [] line = item.Split(";");

                // [0] = 1
                // [1] = VivoKeyd
                // [2] = vivo.jpg

                // Criamos o objeto equipe
                Equipe equipe = new Equipe();

                // Alimentamos o objeto equipe
                equipe.IdEquipe = int.Parse( line[0] );
                equipe.Nome     = line [1];
                equipe.Imagem   = line [2];

                // Adicionamos a equipe na lista de objetos
                equipes.Add(equipe);
            }

            return equipes;

        }

        public void Update(Equipe equipe)
        {   // Exemplos
            //1;Vivo;vivo.jpg
            //1;Vivo;vivo.jpg
            //1;Vivo;vivo.jpg
            //2;Vivo;vivo.jpg
            //1;Vivo;vivo.jpg
            //1;Vivo;vivo.jpg
            
            List<string> linhas = ReadAllLinesCSV(PATH);

            // Removemos a linha que tenha o código a ser alterado
            linhas.RemoveAll(x => x.Split(";")[0] == equipe.IdEquipe.ToString());

            // Adiciona a linha alterada no final do arquivo com mesmo código
            linhas.Add( Prepare(equipe) );

            // Reescreve o csv com as alterações
            ReWriteCSV(PATH, linhas);
        }
    }
}