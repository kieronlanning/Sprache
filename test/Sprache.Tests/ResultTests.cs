﻿using System.Collections.Generic;
using Xunit;

namespace Sprache.Tests;

    public class ResultTests
    {
        [Fact]
        public void FailureContainingBracketFormattedSuccessfully()
        {
            var p = Parse.String("xy").Text().XMany().End();
            var r = (Result<IEnumerable<string>>)p.TryParse("x{");
            Assert.Contains("unexpected '{'", r.Message);
        }

        [Fact]
        public void FailureShowsNearbyParseResults()
        {
            var p = from a in Parse.Char('x')
                    from b in Parse.Char('y')
                    select string.Format("{0},{1}", a, b);

            var r = (Result<string>)p.TryParse("x{");

            const string expectedMessage = @"Parsing failure: unexpected '{'; expected y (Line 1, Column 2); recently consumed: x";

            Assert.Equal(expectedMessage, r.ToString());
        }
    }
