using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ProgMob.Models;
using System.Threading.Tasks;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireCard
    {
        bool InsertCard(Card Card);
        Task<bool> DeleteCard(Card Card);

    }
    public class DatabaseCard
    {

    }
}
