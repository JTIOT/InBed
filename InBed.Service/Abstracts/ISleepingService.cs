using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using InBed.Service.Dto;
using InBed.Entity;

namespace InBed.Service.Abstracts
{
    public partial interface ISleepingService
    {
        ResultDto<SleepingTimeDto> WeekSleepingDate(string f_code, int elderID);
    }
}
