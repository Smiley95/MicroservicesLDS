using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioManager.DBContext;
using PortfolioManager.Models;
using Microsoft.EntityFrameworkCore;
using PortfolioManager.Repositories;

namespace PortfolioManager.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DBPortfolioManager _dbContext;
        public AssetRepository(DBPortfolioManager dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Asset> GetAllAsset()
        {
            _dbContext.Assets.ToList();
            throw new NotImplementedException();
        }

        public Asset GetAssetById(string assetID)
        {
            //_dbContext.Products.Find(productId);
            _dbContext.Assets.Find(assetID);
            throw new NotImplementedException();
        }

        public void InsertAsset(Asset asset)
        {
            _dbContext.Assets.Add(asset);
            Save();
            throw new NotImplementedException();
        }

        public void UpdateAsset(Asset asset)
        {
            _dbContext.Entry(asset).State = EntityState.Modified;
            Save();
            throw new NotImplementedException();
        }

        public void DeleteAsset(string assetID)
        {
            var asset = _dbContext.Assets.Find(assetID);
            _dbContext.Assets.Remove(asset);
            Save();
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
