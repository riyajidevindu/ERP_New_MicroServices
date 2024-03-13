using AutoMapper;
using ERP.TranscriptGeneration.Application;
using ERP.TranscriptGenetation.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF;

namespace ERP.TranscriptGeneration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranscriptController: BaseController
    {

        public TranscriptController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }





        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            //var results = await _unitOfWork.Results.GetAllAsync();
            Settings.License = LicenseType.Community;


            
            var document = new TranscriptDocument(_unitOfWork);


            document.GeneratePdf(@"c:\temp\transcript.pdf");
            
            return NoContent();
        }
    }
}
