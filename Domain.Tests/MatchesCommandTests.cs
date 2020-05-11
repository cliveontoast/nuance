using Autofac;
using Domain.Commands;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Domain.Tests
{
    [TestClass]
    public class MatchesCommandTests
    {
        public ILifetimeScope scope;

        [TestCleanup]
        public void Cleanup()
        {
            scope?.Dispose();
        }

        [TestInitialize]
        public void Init()
        {
            var container = TestSetup.GetContainerBuilder().Build();
            scope = container.BeginLifetimeScope();
        }

        [TestMethod]
        [DataRow("hello hello", "LO", 3, 9)]
        [DataRow("hello hello", "Lo", 3, 9)]
        [DataRow("hello hello", "lO", 3, 9)]
        [DataRow("helLO hello", "LO", 3, 9)]
        [DataRow("helLo hellO", "LO", 3, 9)]
        [DataRow("hellO hello", "LO", 3, 9)]
        [DataRow("hello hellO", "LO", 3, 9)]
        public void MatchesCommand_CaseInsensitive_Success(object text, params object[] subtext)
        {
            MatchCommandResults(text, subtext);
        }

        [TestMethod]
        [DataRow("hello", "lo", 3)]
        [DataRow("hello hello", "lo", 3, 9)]
        [DataRow(".. hello", ".", 0, 1)]
        [DataRow("1", "1", 0)]
        public void MatchesCommand_SingleLine_Success(object text, params object[] subtext)
        {
            MatchCommandResults(text, subtext);
        }

        [TestMethod]
        [DataRow(@"hello
hello", "lo", 3, 10)]
        public void MatchesCommand_MultiLine_Success(object text, params object[] subtext)
        {
            MatchCommandResults(text, subtext);
        }

        private void MatchCommandResults(object text, object[] subtext)
        {
            var subtextInput = subtext.First() as string;

            var result = scope.RunSync<MatchesCommand, MatchResult>(new MatchesCommand
            {
                Subtext = subtextInput,
                Text = text.ToString(),
            });

            var subtextResults = subtext.Skip(1).Cast<int>().ToArray();
            Assert.AreEqual(subtextResults.Length, result.CharacterPositions.Count(), "Length misaligned");
            for (int i = 0; i < subtextResults.Length; i++)
            {
                Assert.AreEqual(subtextResults[i], result.CharacterPositions[i], "character position array");
            }
        }

        [TestMethod]
        [DataRow("one", "two")]
        [DataRow("1", "2")]
        public void MatchesCommand_SubtextNotInText_NoMatches(string text, string subtext)
        {
            var result = scope.RunSync<MatchesCommand, MatchResult>(new MatchesCommand
            {
                Subtext = subtext,
                Text = text
            });
            Assert.AreEqual(0, result.CharacterPositions.Count);
        }

        [TestMethod]
        [DataRow(null, null)]
        [DataRow("", "")]
        [DataRow(null, "")]
        [DataRow("", null)]
        [DataRow("text", null)]
        [DataRow("text", "")]
        public void MatchesCommand_BlankSubtext_ErrorResult(string text, string subtext)
        {
            var result = scope.RunSync<MatchesCommand, MatchResult>(new MatchesCommand
            {
                Subtext = subtext,
                Text = text
            });
            Assert.AreEqual(MatchResultErrorEnum.SubtextNullOrEmpty, result.Error);
        }
    }
}
