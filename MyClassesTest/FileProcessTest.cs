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
        public void FileNameNullOrEmpty_throwsArgumentNullException() { //Nome do meu arquivo nulo ou nao existe

            //Todo
            Assert.Inconclusive();
        }
    }
}
