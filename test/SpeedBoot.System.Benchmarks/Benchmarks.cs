// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Benchmarks;

public class Benchmarks
{
    static HttpContext _httpContext;
    static FromQuery<UserQuery> _userQuery;

    [GlobalSetup]
    public void Initialized()
    {
        var dictionary = new Dictionary<string, StringValues>();
        dictionary.Add(nameof(UserQuery.Id), new StringValues("1"));
        dictionary.Add(nameof(UserQuery.Name), new StringValues("SpeedBoot"));
        dictionary.Add(nameof(UserQuery.Gender), new StringValues(true.ToString()));
        dictionary.Add(nameof(UserQuery.Tags), new StringValues(["SpeedBoot", "SpeedBoot2"]));
        dictionary.Add(nameof(UserQuery.Times), new StringValues(["2022-01-01", "2022-01-02"]));
        _httpContext = new DefaultHttpContext()
        {
            Request =
            {
                Query = new QueryCollection(dictionary)
            }
        };
        _userQuery = new();
        GetBindWithLambda();
    }

    [Benchmark(Baseline = true)]
    public void GetBind()
    {
        var userQuery = new UserQuery();
        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Id), out var idStr) && !string.IsNullOrWhiteSpace(idStr))
        {
            userQuery.Id = int.Parse(idStr);
        }
        if (_httpContext.Request.Query.TryGetValue(nameof(UserQuery.Name), out var name) && !string.IsNullOrWhiteSpace(name))
        {
            userQuery.Name = name;
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
            userQuery.Times = timeValues.Select(DateTime.Parse!).ToList();
        }
    }

    [Benchmark]
    public void GetBindWithLambda()
    {
        _ = FromQuery<UserQuery>.BindAsync(_httpContext).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}
