using System;
using System.IO;

namespace MyClasses {
    public class FileProcess {
        public bool FileExists(string fileName) {

            //verifica se é vazio ou nulo
            if (string.IsNullOrEmpty(fileName)) {

                throw new ArgumentNullException("fileName");
            }
            return File.Exists(fileName);
        }
    }
}
