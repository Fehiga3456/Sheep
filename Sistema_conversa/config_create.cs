using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class config_create : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //se o arquivo config nao existe, vamos inicializa-lo com alguns valores padrao
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "config")))
        {
            //valores a adicionar
            string[] valores = { "preferred_language=pt_br" };
            //comando de criacao
            File.WriteAllLines(Path.Combine(Application.persistentDataPath, "config"), valores);
        }
    }
}
