using System;

namespace ProjetoParaEstudo.Core.Interfaces
{
    public interface IAutenticacaoService
    {
        string GerarJwtToken(string email, Guid idUsuario);
        string EmcriptarSha256Hash(string senha);
    }
}
