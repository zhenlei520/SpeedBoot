// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using COSXML.Model.Tag;
using COSXML.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedBoot.ObjectStorage.Tencent
{
    public class Methods
    {
        public void Delete()
        {
            DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
            DeleteObjectStorageRequest deleteObjectStorageRequest = new DeleteObjectStorageRequest()
            {
                BucketName= "examplebucket-1250000000",
                ObjectName= "exampleobject"
            };
            defaultObjectStorageClient.Delete(deleteObjectStorageRequest);
        }
    }
}
