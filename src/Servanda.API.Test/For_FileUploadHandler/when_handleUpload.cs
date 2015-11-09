//using FakeItEasy;
//using Machine.Specifications;
//using Microsoft.AspNet.Hosting;
//using Servanda.API.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Servanda.API.Test.For_FileUploadHandler
//{
//    [Subject(typeof(FileUploadHandler), "FileUploadHandler")]
//    public class when_handleUpload
//    {
//        protected static IFileUploadHandler _fileUploadHandler;
//        protected static IStreamHandler _streamHandler;
//        protected static IHostingEnvironment _hostingEnvironment;

//        Establish context = () =>
//        {
//            _streamHandler = A.Fake<IStreamHandler>();
//            _hostingEnvironment = A.Fake<IHostingEnvironment>();

//            _fileUploadHandler = new FileUploadHandler(_streamHandler, _hostingEnvironment);
//        };

//        Because of = () =>
//        {
            
//        };

//    }
//}
