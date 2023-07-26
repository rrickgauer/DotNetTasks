using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.TableView;

public interface ITableView
{
    public string ViewName { get; }
}
