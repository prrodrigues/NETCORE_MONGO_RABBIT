using System.Text.RegularExpressions;
using MongoDB.Bson;
using Test.RentMotorCycles.Infrastructure;

namespace Test.RentMotorCycles.Domain.Entity;

public class Entregador
{
    public ObjectId _id { get; set; }

    public string identificador { get; set; }

    public string nome { get; set; }

    public string cnpj { get; set; }

    public string data_nascimento { get; set; }

    public string numero_cnh { get; set; }

    public string tipo_cnh { get; set; }

    //id img url 
    public string imagem_cnh { get; set; }

    public bool Validate()
    {
        if (this.identificador == null) throw new ArgumentNullException(nameof(this.identificador));
        if (this.nome == null) throw new ArgumentNullException(nameof(this.nome));
        if (this.cnpj == null) throw new ArgumentNullException(nameof(this.cnpj));
        if (!Regex.IsMatch(this.cnpj.ToUpper(), @"^-?[0-9][0-9,\.]+$")) throw new Exception("CNPJ deve possuir apenas numeros");
        if (this.data_nascimento == null) throw new ArgumentNullException(nameof(this.data_nascimento));
        if (this.numero_cnh == null) throw new ArgumentNullException(nameof(this.numero_cnh));
        if (this.tipo_cnh == null) throw new ArgumentNullException(nameof(this.tipo_cnh));
        this.tipo_cnh = this.tipo_cnh.ToUpper();
        if (!Regex.IsMatch(this.tipo_cnh.ToUpper(), @"[AB]")) throw new Exception("Categoria de Habilitação não compatível");
        if (this.imagem_cnh == null) throw new ArgumentNullException(nameof(this.imagem_cnh));
        if (!ValidExtension(this.imagem_cnh)) throw new IOException("Imagens no formato PNG ou BMP.");
        if (!IsBase64String(this.imagem_cnh)) throw new IOException("Arquivo inválido.");
        return true;
    }


    bool ValidExtension(string base64String)
    {
        int pos = base64String.IndexOf("base64,");
        pos = pos != -1 ? pos + 7 : 0;
        var data = pos == 0 ? base64String : base64String.Substring(pos, base64String.Length - pos);
        var dataextension = base64String.Substring(pos, 1);
        String[] files = ["I", "Q"];
        return files.Contains(dataextension.ToUpper());
    }

    bool IsBase64String(string base64String)
    {
        try
        {
            int pos = base64String.IndexOf("base64,");
            pos = pos != -1 ? pos + 7 : 0;
            var data = pos == 0 ? base64String : base64String.Substring(pos, base64String.Length - pos);

            Convert.FromBase64String(data);
            return true;
        }
        catch { }
        return false;
    }



}
