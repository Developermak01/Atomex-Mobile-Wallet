﻿using System;
using System.Collections.Generic;
using System.Linq;
using Atomex.Core;
using Microsoft.Extensions.FileProviders;

namespace atomex.ViewModel
{
    public class DappInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Network Network { get; set; }
        public bool IsActive { get; set; }
        public DappType DappDeviceType { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; set; }

        public string PermissionsFormatted => Permissions != null
            ? string.Join(", ", Permissions.Select(x => x.Name)) 
            : string.Empty;

        public static List<DappInfo> MockDapps() =>
            new List<DappInfo>()
            {
                new() { Name = "abcd", Network = Network.TestNet, ImageUrl = "LTC", IsActive = true, DappDeviceType = DappType.Desktop, Permissions = new List<Permission>() { new Permission() {Name="Ты можешь читать"}, new Permission() {Name="Ты можешь писать"}, new Permission() {Name="Ты не можешь отсосать"}}},
                new() { Name = "xyz", Network = Network.MainNet, ImageUrl = "BTC", IsActive = true, DappDeviceType = DappType.Mobile},
                new() { Name = "Desktop", Network = Network.MainNet, ImageUrl = "ETH", IsActive = true, DappDeviceType = DappType.Web},
                new() { Name = "xyz5", Network = Network.MainNet, ImageUrl = "LTC", IsActive = true, DappDeviceType = DappType.Mobile},
                new() { Name = "xyz4", Network = Network.MainNet, ImageUrl = "LTC", IsActive = true, DappDeviceType = DappType.Mobile},
                new() { Name = "xyz3", Network = Network.MainNet, ImageUrl = "LTC", IsActive = true, DappDeviceType = DappType.Mobile},
                new() { Name = "xyz2", Network = Network.MainNet, ImageUrl = "LTC", IsActive = true, DappDeviceType = DappType.Mobile},
                new() { Name = "xyz43", Network = Network.MainNet,ImageUrl = "LTC", IsActive = true, DappDeviceType = DappType.Mobile},
            };
    }

    public enum DappType
    {
        Mobile,
        Desktop,
        Web
    }

    public class Permission
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}