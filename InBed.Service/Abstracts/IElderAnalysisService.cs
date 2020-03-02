using InBed.Service.Dto;
using InBed.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Abstracts
{
    public partial interface IElderAnalysisService

    {
        ResultDto<ElderAnalysisDto> GetWithPages(ElderAnalysisRequest request);

        ResultDto<ElderAnalysisDto> ElderAnalysis(string f_code, string elderName, string S_data, string E_data);
    }
}
