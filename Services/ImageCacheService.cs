using Poker.Model;
using System.Resources;

namespace Poker.Services;
public static class ImageCacheService
{
    private static readonly Dictionary<Card, Image> _cache = [];
    private static readonly ResourceManager _manager = Properties.Resources.ResourceManager;

    public static Image Get(Card card)
    {
        if(_cache.TryGetValue(card, out var image))
            return image;
        if(Get(card.ImageName) is Image i)
        {
            _cache[card] = i;
            return i;
        }
        throw new Exception($"No such card named {card.ImageName}");
    }


    private static Image? Get(string fileName) => (Image?)_manager.GetObject(fileName);
}
