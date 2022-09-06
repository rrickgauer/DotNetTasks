using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using static Tasks.Domain.Responses.RepositoryResponses;

namespace Tasks.Repositories.Interfaces;

public interface ILabelRepository
{
    public Task<SelectAllResponse> SelectLabelsAsync(Guid userId);
    public Task<ModifyResponse> ModifyLabelAsync(Label label);
    public Task<SelectResponse> SelectLabelAsync(Guid labelId);
}
