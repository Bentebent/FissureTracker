using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FissureTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GetFissureDeathsAsync();
            Console.ReadLine();
        }

        private static async Task GetFissureDeathsAsync()
        {
            string apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI5MjYwNzU2YS0xNjhkLTRhZmItYWI2Yi1mNjFjNjM1OWQ5ODkiLCJqdGkiOiI1NWFhNjA1YTIyY2Y5ODRhYjVkZjEzNjRhOTE0YWUwNjNmMmMyY2Q3NjcxMTBjYzE3NjBiYTk0ZWU1Y2NhMzZhYTM2YWE4NDA2ZmNjYTE3ZSIsImlhdCI6MTYwOTQ0ODUxNywibmJmIjoxNjA5NDQ4NTE3LCJleHAiOjE2MTIwNDA1MTcsInN1YiI6IjEzNDMzNDYiLCJzY29wZXMiOlsidmlldy1wcml2YXRlLXJlcG9ydHMiXX0.jLX2kzVS4YfnSpZKzGDBDf29Yd2bfkehujKp7hqpQfDjb8zidH8vlfnabBN0ECrhkjd9GyXW2vbACs40kmhOnMiAdlOc0AJWvlzmT3efcokWjSjAiSFgPORmRE6PVfRGmFSmW6mAGexRMecHHi9vpkwOxpPJDGnRX1eyj4xGEqbwn7b0PEzpp3zLz1rmvL2WNSVdUnQNnockWHHsNhNLNgPXSqaiNyLardEdsEWZUgJ_X6zWFF8gKKOMoDFZu3OAir501WeaET8aBCdlcqBFTBzMWU_jOQ1RMhUiP42KPLrrxQtXCKa4oWSG1-fbe7FWqN4IYhCAlzmrH4o2wWOGGnh09nKPFiX8Ia4DWd8ADsyLDBu_4x9wMo7eLA-abCam-Dq9rVSsNmgWQQOkpCDgU8rIshKcfciEe2MlcCRgfGmlr5PBd_nVTDtwawoJkS_aW5gkWh2qIYHZ9E-c-k0081bzrzoxGcXu13BhG3IHjRa_bSM8RvqbmhL4EkZkdhctEUj3KqzOpUf39OA1RSNBLhfpV382ExGxvOd4YkivQNLZPk1-462dq4FZUQdllOHwYJYTdxtUDGv2qCeXfIoBw4nqDT55w0a4yikbo8ORATzTVdJnjDD4_Jkr3SBGjmENYUIrC9YjcTDm4ET2pXNyYS2C5L2mzMz-dLne4p3p9HY";
            string graphQLEndpoint = "https://classic.warcraftlogs.com/api/v2/client";

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(graphQLEndpoint)
            };

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await client.GetAsync(
                       @"?query={
                          reportData {
                            reports(guildID:484175, zoneID: 1006){ 
                              data {
                                title,
                                code,
                                table(startTime: 0, endTime: 9999999999999, encounterID:1114, abilityID:27812, killType:All, dataType:Deaths) 
                              }
                            }
                          }
                        }");

            var stringResult = await response.Content.ReadAsStringAsync();

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(stringResult);
            List<Datum> reports = myDeserializedClass.data.reportData.reports.data.Where(x => x.table.data.entries.Count > 0).ToList();

            Dictionary<string, int> deathsPerPlayer = new Dictionary<string, int>();
            reports.ForEach(x =>
            {
                x.table.data.entries.ForEach(y =>
                {
                    if (!deathsPerPlayer.ContainsKey(y.name))
                    {
                        deathsPerPlayer.TryAdd(y.name, 1);
                    }
                    else
                    {
                        deathsPerPlayer[y.name] += 1;
                    }
                });
            });

            Console.WriteLine(JsonConvert.SerializeObject(deathsPerPlayer));
        }
    }
}
