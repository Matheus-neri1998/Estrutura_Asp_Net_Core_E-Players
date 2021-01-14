using System;
using System.IO;
using E_Players_Asp_Net_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Players_Asp_Net_Core.Controllers
{
    // http://Localhost:5001/Equipe = raíz do projeto. Acesso a "Equipe"
    [Route("Equipe")]
    public class EquipeController : Controller
    {

        // Criamos uma instancia equipeModel com a estrutura Equipe

        Equipe equipeModel = new Equipe();
 
        // http://Localhost:5001/Equipe/Listar
        [Route("Listar")]
        public IActionResult Index()
        {   
            // Listando todas as equipes e enviando para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll(); 
            return View();
        }

        // http://Localhost:5001/Equipe/Cadastrar
        [Route("Cadastrar")]

        public IActionResult Cadastrar(IFormCollection form) // Método de cadastrar
        {
            // Criamos uma nova instancia de Equipe e armazenamos
            // os dados enviados pelo usuário através de formulário e 
            // salvamos no objeto newTeam
            Equipe newTeam = new Equipe();
            newTeam.IdEquipe = Int32.Parse( form["IdEquipe"] ); // conversão de int para string
            newTeam.Nome = form["Nome"]; // recebe o Nome

            
            // Upload início
            // Verificamos se o usuário anexou um arquivo
            if ( form.Files.Count > 0 )
            {
                // Se sim, armazenamos o arquivo na variável file
                var file  = form.Files[0];
                var folder = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img/Equipes" );

                // Verificamos se a pasta Equipes não existe
                if (!Directory.Exists(folder))
                {
                    // Se não existe a pasta, a criamos
                    Directory.CreateDirectory(folder);
                }
                                                    // localhost: 5001   +                +  Equipes + equipe.jpg
                var caminho = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName );
                
                using (var stream = new FileStream(caminho, FileMode.Create))
                {
                    // Salvamos o arquivo no caminho especificado 
                    file.CopyTo(stream);
                }

                newTeam.Imagem = file.FileName;
            }
            else
            {
                newTeam.Imagem = "padrao.png";
            }
            // Upload término


            // Chamamos o método Create para salvar o newTeam no CSV
            equipeModel.Create(newTeam); // método de criar o cadastro
            ViewBag.Equipes = equipeModel.ReadAll(); // armazena o cadastro

            return LocalRedirect("~/Equipe/Listar");  // Redirecionar para alguma página
        }

        // http://Localhost:5001/Equipe/1
        [Route("{id}")]

        public IActionResult Excluir(int id) // Método de excluir
        {
            equipeModel.Delete(id);

            ViewBag.Equipes = equipeModel.ReadAll(); 

            return LocalRedirect("~/Equipe/Listar");


        }
    }
}