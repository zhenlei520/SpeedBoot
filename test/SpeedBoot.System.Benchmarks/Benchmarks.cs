// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Benchmarks;

public class Benchmarks
{
    HttpContext _httpContext;

    [GlobalSetup]
    public void Initialized()
    {
        var dictionary = new Dictionary<string, StringValues>
        {
            {
                nameof(UserQuery.Id), new StringValues("1")
            },
            {
                nameof(UserQuery.Name), new StringValues("SpeedBoot")
            },
            {
                nameof(UserQuery.Gender), new StringValues(true.ToString())
            },
            {
                nameof(UserQuery.Tags), new StringValues(["SpeedBoot", "SpeedBoot2"])
            },
            {
                nameof(UserQuery.Times), new StringValues(["2022-01-01", "2022-01-02"])
            }
        };
        _httpContext = new DefaultHttpContext()
        {
            Request =
            {
                Query = new QueryCollection(dictionary)
            }
        };
        DynamicBindWithLambda();
    }

    [Benchmark(Baseline = true)]
    public void HardCodeBind()
    {
        var userQuery = new UserQuery();
        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Id), out var idStr) && !string.IsNullOrWhiteSpace(idStr))
        {
            userQuery.Id = int.Parse(idStr);
        }

        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Name), out var nameStr) && !string.IsNullOrWhiteSpace(nameStr))
        {
            userQuery.Name = nameStr;
        }

        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Gender), out var genderStr) && !string.IsNullOrWhiteSpace(genderStr))
        {
            userQuery.Gender = bool.Parse(genderStr);
        }

        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Tags), out var tagValues) && tagValues.Count > 0)
        {
            userQuery.Tags = tagValues.Select(tag => tag!.ToString()).ToArray();
        }

        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Times), out var timeValues) && timeValues.Count > 0)
        {
            userQuery.Times = timeValues.Select(v => string.IsNullOrWhiteSpace(v) ? default(DateTime?) : DateTime.Parse(v)).ToList();
        }
    }

    [Benchmark]
    public void DynamicBindWithLambda()
    {
        _ = FromQuery<UserQuery>.BindAsync(_httpContext).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}
