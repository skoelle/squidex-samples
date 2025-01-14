﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System.IO;
using System.Threading.Tasks;
using Squidex.ClientLibrary.Management;

namespace Squidex.CLI.Commands.Implementation.Sync.Assets
{
    public static class Extensions
    {
        public static FileInfo GetBlobFile(this DirectoryInfo directoryInfo, string id)
        {
            return new FileInfo(Path.Combine(directoryInfo.FullName, $"assets/files/{id}.blob"));
        }

        public static BulkUpdateAssetsJobDto ToMoveJob(this AssetModel model)
        {
            return new BulkUpdateAssetsJobDto
            {
                Id = model.Id,
                Type = BulkUpdateAssetType.Move,
                ParentPath = model.FolderPath
            };
        }

        public static BulkUpdateAssetsJobDto ToAnnotateJob(this AssetModel model)
        {
            return new BulkUpdateAssetsJobDto
            {
                Id = model.Id,
                Type = BulkUpdateAssetType.Annotate,
                FileName = model.FileName,
                IsProtected = model.IsProtected,
                ParentId = null,
                ParentPath = null,
                Metadata = model.Metadata,
                Slug = model.Slug,
                Tags = model.Tags
            };
        }

        public static async Task<AssetModel> ToModelAsync(this AssetDto asset, FolderTree folders)
        {
            return new AssetModel
            {
                Id = asset.Id,
                Metadata = asset.Metadata,
                MimeType = asset.MimeType,
                Slug = asset.Slug,
                FileName = asset.FileName,
                FileHash = asset.FileHash,
                FolderPath = await folders.GetPathAsync(asset.ParentId),
                Tags = asset.Tags,
                IsProtected = asset.IsProtected
            };
        }
    }
}
