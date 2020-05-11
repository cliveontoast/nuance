using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class MatchesCommand : IRequest<MatchResult>
    {
        public string Text { get; set; }
        public string Subtext { get; set; }
    }

    public class MatchescommandHandler : IRequestHandler<MatchesCommand, MatchResult>
    {
        public Task<MatchResult> Handle(MatchesCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Subtext))
                return Task.FromResult(new MatchResult
                {
                    Error = MatchResultErrorEnum.SubtextNullOrEmpty
                });

            if (string.IsNullOrEmpty(request.Text))
                return Task.FromResult(new MatchResult
                {
                    CharacterPositions = new int[0]
                });

            int index = -1;
            var result = new List<int>();
            do
            {
                result.Add(index);
                index = request.Text.IndexOf(request.Subtext, index + 1, StringComparison.InvariantCultureIgnoreCase);
            }
            while (index != -1);

            return Task.FromResult(new MatchResult
            {
                CharacterPositions = result.Skip(1).ToArray()
            });
        }
    }
}
