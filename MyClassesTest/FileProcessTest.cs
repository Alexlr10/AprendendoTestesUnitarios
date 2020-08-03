using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest {
    [TestClass]
    public class FileProcessTest {
        [TestMethod]
        public void FileNameDoesExists() { //Nome do meu arquivo existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();
            bool fromCall;
            //Verifica se existe ou nao o arquivo Regedit no diretorio passado
            fromCall = fp.FileExists(@"C:\Windows\Regedit.exe");
           //Verifica se o arquivo e True ou False
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExists() { //Nome do meu arquivo nao existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();
            bool fromCall;
            //Verifica se existe ou nao o arquivo Regedit no diretorio passado
            fromCall = fp.FileExists(@"C:\Regedit.exe");
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
