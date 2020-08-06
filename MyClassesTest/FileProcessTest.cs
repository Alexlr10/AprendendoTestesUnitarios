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

        public TestContext TestContext { get; set; }

        #region Test Initialize e Cleanup
        [TestInitialize]
        public void TestInitialize() {
            if (TestContext.TestName == "FileNameDoesExists") {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName)) {           
                    TestContext.WriteLine($"Criando arquivo: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some text");
                }
            }
        }
        [TestCleanup]
        public void TestCleanup() {
            if (TestContext.TestName == "FileNameDoesExists") {
                if (!string.IsNullOrEmpty(_GoodFileName)) {
                    TestContext.WriteLine($"Deletando arquivo: {_GoodFileName}");
                    File.Delete(_GoodFileName);
                }
            }
        }
        #endregion

        [TestMethod]
        [Description("Verificando se o arquivo existe")]
        [Owner("Alex Lopes")]
        [Priority(0)]
        [TestCategory("Nenhuma Excessao")]
        public void FileNameDoesExists() { //Nome do meu arquivo existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();
            bool fromCall;
            //Verifica se existe ou nao o arquivo no diretorio passado
            fromCall = fp.FileExists(_GoodFileName);
            TestContext.WriteLine($"Testando arquivo: {_GoodFileName}");      

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

        private const string FILE_NAME = @"FileToDeploy.txt";

        [TestMethod]
        [Owner("Alex")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistsUsingDeploymentItem() {
            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = $@"{TestContext.DeploymentDirectory}\{FILE_NAME}";
            TestContext.WriteLine($"Verificando arquivo: {fileName}");

            fromCall = fp.FileExists(fileName);
            //Verifica se o arquivo e True ou False
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Timeout(3100)]
        public void SimulateTimeOut() {
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        [Description("Verificando se o arquivo não existe")]
        [Owner("Alex Lopes")]
        [Priority(1)]
        [TestCategory("Nenhuma Excessao")]
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
        [Owner("Alex Lopes")]
        [Priority(0)]
        [TestCategory("Excessao")]
        public void FileNameNullOrEmpty_throwsArgumentNullException() { //Nome do meu arquivo nulo ou nao existe

            //Instancia um objeto fp de FileProcess
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        [Priority(1)]
        [TestCategory("Excessao")]
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
