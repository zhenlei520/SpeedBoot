// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun;

public class DefaultAliyunAcsClientFactory: IAliyunAcsClientFactory
{
    public IAcsClient GetAcsClient(string accessKeyId, string accessKeySecret, string regionId)
    {
        var profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
        return new DefaultAcsClient(profile);
    }
}
