using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Databases;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.General;

[RegisterInjectable(typeof(CardHandler))]
public class CardHandler
{
    private readonly AminosDB aminosDb;

    public CardHandler(AminosDB aminosDb)
    {
        this.aminosDb = aminosDb;
    }

    public async ValueTask<CommonApiResponse> BindCardToUser(UserAccount user, string accessCode)
    {
        if (user is null)
            return new CommonApiResponse(false, "用户未登录");
        var card = await aminosDb.Cards.FirstOrDefaultAsync(x => x.Luid == accessCode);
        if (card is null)
            return new CommonApiResponse(false, "卡号并未在本服务器游玩过");
        if (await aminosDb.UserAccounts.AnyAsync(x => x.Cards.Contains(card)))
            return new CommonApiResponse(false, "此卡号已被绑定");

        //todo 检查此卡是否刚用过(10分钟内)。避免有坏比闲的开撞
        user.Cards.Add(card);
        await aminosDb.SaveChangesAsync();

        return new CommonApiResponse(true);
    }

    public async ValueTask<CommonApiResponse> UnbindCardToUser(UserAccount user, string accessCode)
    {
        if (user is null)
            return new CommonApiResponse(false, "用户未登录");
        var card = await aminosDb.Cards.FirstOrDefaultAsync(x => x.Luid == accessCode);
        if (card is null)
            return new CommonApiResponse(false, "卡号并未在本服务器游玩过");
        if (!user.Cards.Contains(card))
            return new CommonApiResponse(false, "用户并未绑定过此卡号");

        user.Cards.Remove(card);
        await aminosDb.SaveChangesAsync();

        return new CommonApiResponse(true);
    }

    public async ValueTask<CommonApiResponse> GetCards(UserAccount user)
    {
        if (user is null)
            return new CommonApiResponse(false, "用户未登录");

        return new CommonApiResponse<Card[]>(true, user.Cards.ToArray());
    }
}