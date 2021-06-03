// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.13.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot1.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var luisResult = LUIS.MakeRequest(turnContext.Activity.Text);
            var replyText = $"您說了 :  {turnContext.Activity.Text}";

            foreach (var item in luisResult.entities)
            {
                replyText += $"{System.Environment.NewLine} entity: {item.entity} - {item.score}";
            }
            foreach (var item in luisResult.intents)
            {
                replyText += $"{System.Environment.NewLine} intent: {item.intent} - {item.score}";
            }
            replyText += $"{System.Environment.NewLine} topScoringIntent: {luisResult.topScoringIntent.intent} - {luisResult.topScoringIntent.score}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
