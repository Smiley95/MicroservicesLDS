using PortfolioManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManager.Repositories
{
    public interface IAssetRepository
    {
        IEnumerable<Asset> GetAllAsset();
        Asset GetAssetById(string assetID);
        void InsertAsset(Asset asset);
        void UpdateAsset(Asset asset);
        void DeleteAsset(string assetID);
        void Save();
    }
}
