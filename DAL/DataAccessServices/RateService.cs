using SolidTA.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SolidTA.DAL.DataAccessServices
{
    public class RateService
    {
        private RateService() { }

        public static async Task<bool> AddRates(List<Rate> rates)
        {
            
            using (DbSolidContext dbContext = new DbSolidContext())
            {
                if (!dbContext.Database.Exists())
                    return false;
                foreach (var rate in rates)
                {
                    Currency dbCurrency =await dbContext.Currency.FirstOrDefaultAsync(c => c.CharCode.Equals(rate.Currency.CharCode));
                    if (dbCurrency == null)
                    {
                        dbCurrency = dbContext.Currency.Add(rate.Currency);
                        await dbContext.SaveChangesAsync();

                    }
                    rate.Currency = dbCurrency;
                    rate.CurrencyID = dbCurrency.CurrencyID;
                    Rate dbRate = await dbContext.Rate.FirstOrDefaultAsync(r=> r.CurrencyID==rate.CurrencyID && r.Date == rate.Date);
                    if (dbRate == null)
                    {
                        dbContext.Rate.Add(rate);
                    }
                    else
                    {
                        rate.RateID = dbRate.RateID;
                        dbContext.Entry(dbRate).CurrentValues.SetValues(rate);

                    }
                    //await dbContext.SaveChangesAsync();

                }
                await dbContext.SaveChangesAsync();
                return true;
            }

        }
    }
}
