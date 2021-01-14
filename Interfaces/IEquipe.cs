using System.Collections.Generic;
using E_Players_Asp_Net_Core.Models;

namespace E_Players_Asp_Net_Core.Interfaces
{
    public interface IEquipe
    {
        // CRUD

        void Create(Equipe Team);
        List<Equipe> ReadAll();
        void Update(Equipe equipe);
        void Delete(int id); // Identificador da equipe

    }
}