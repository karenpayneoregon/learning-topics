using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworksLifeApp.Models;

namespace FrameworksLifeApp.Interfaces;
public interface IDotNetReleaseMetadataService
{
    Task<IReadOnlyList<DotNetChannelInfo>> GetChannelsAsync(CancellationToken cancellationToken = default);
}