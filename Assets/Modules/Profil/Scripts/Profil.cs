using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aloha
{
    public class Profil
    {
        public string name;

        public Profil()
        {
            this.name = "default-profil";
        }

        public Profil(string name)
        {
            this.name = name;
        }
    }
}