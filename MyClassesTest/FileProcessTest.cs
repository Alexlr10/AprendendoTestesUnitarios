using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest {
    [TestClass]
    public class FileProcessTest {
        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private string _GoodFileName;
        [TestMethod]
        public void FileNameDoesExists() { //Nome do meu arquivo existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();
            bool fromCall;
            SetGoodFileName();
            File.AppendAllText(_GoodFileName, "Some text");

            //Verifica se existe ou nao o arquivo no diretorio passado
            fromCall = fp.FileExists(_GoodFileName);
            File.Delete(_GoodFileName);

           //Verifica se o arquivo e True ou False
            Assert.IsTrue(fromCall);
        }

        public void SetGoodFileName() {
            //Pega o conteudo do arquivo de configuraçao por meio da chave
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            //verifica se ele contem um appPath
            if (_GoodFileName.Contains("[appPath]")) {
                //executa um replace pegando o local da aplicaçao
                _GoodFileName = _GoodFileName.Replace("[appPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        public void FileNameDoesNotExists() { //Nome do meu arquivo nao existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();
            bool fromCall;
            //Verifica se existe ou nao o arquivo Regedit no diretorio passado
            fromCall = fp.FileExists(BAD_FILE_NAME);
            //Verifica se o arquivo e True ou False
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_throwsArgumentNullException() { //Nome do meu arquivo nulo ou nao existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmpty_throwsArgumentNullException_UsingTryCatch() { //Nome do meu arquivo nulo ou nao existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();

            try {
                fp.FileExists("");
            } catch (ArgumentException) {

                return;
            }
            Assert.Fail("Erro esperada");
        }
    }
}
