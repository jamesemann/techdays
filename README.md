# TechDays Online 2016 Bots

Accompanying code and resources for the three sessions Monday 13th September at TechDays Online 2016.

The links are presented in the order that they appear in the sessions.

##Session Summary

### Session 1 - The Why of Bots
An overview of intelligence in organisations, Why Bots are important and how they will help an organisation.

- [Bot Framework](https://dev.botframework.com/)
- [Bot Application Visual Studio 2015 Template](http://aka.ms/bf-bc-vstemplate)
- [Bot Framework Channel Emulator](https://aka.ms/bf-bc-emulator)

### Session 2 - The How of Bots

A run through of how to build a bot and where it can be used and how natural language fits in to building a more natural Bot.

- [LUIS](https://www.luis.ai/)
- [Cognitive Services](https://www.microsoft.com/cognitive-services)

### Session 3 - The When of Bots
A set of demonstrations on Building and using natural language and sentiment analysis to enhance a Bot.

- [Cognitive Services Text Analytics](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api)

##Code and configuration shown in Sessions

The code and resource for each demo are in /TechDays.Bots/.  

###Demo1.HelloBotFramework
Introduces the Bot Framework using a simple Bot.
To get this working you will need to:
- Add the BotId, MicrosoftAppId, MicrosoftAppPassword defined in Bot registration to the web.config.

##Demo2.LUIS
Luis model and utterances sample to train the Bot.  You can import both these files through the Luis portal.

##Demo3.IntelligentBot
Demonstrates adding intelligence to your Bot by linking a Bot Application to LUIS model by use of LuisDialog.
To get this working you will need to:
- Add the BotId, MicrosoftAppId, MicrosoftAppPassword defined in Bot registration to the web.config.
- Add your LUIS Model Id & LUIS Subscription Key to the SalesLuisDialog

##Demo4.SentimentBot
Demonstrates adding sentiment analysis using a third party component - Cogntive Services Text Analytics.
To get this working you will need to:
- Add the BotId, MicrosoftAppId, MicrosoftAppPassword defined in Bot registration to the web.config.
- Add the CognitiveServicesTextAnalyticsKey to the web.config
