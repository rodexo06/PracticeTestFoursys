using MediatR;
using PracticeTestFoursys.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Application.Query
{
    public class GetPositionbyClientQuery : IRequest<List<PositionModel>>
    {
        public string ClientId { get; set; }
    }
}
