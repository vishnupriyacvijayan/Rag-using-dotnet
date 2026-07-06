using MongoDB.Bson;
using MongoDB.Driver;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using OpenAI.Embeddings;

MyBakeryApp myChatApp = new MyBakeryApp();

while (true)
{
    Console.Write("Query: ");
    string? inputQuery = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(inputQuery))
        continue;

    if (inputQuery.Equals("quit", StringComparison.OrdinalIgnoreCase))
        break;

    string answer = await myChatApp.ChatFunction(inputQuery);

    Console.WriteLine($"Answer: {answer}");
}

public class  MyBakeryApp
{
    private readonly EmbeddingClient _embeddingClient;
    private readonly IMongoCollection<BsonDocument> _bakeryCollection; 
    private readonly ChatClient _chatClient;

    public MyBakeryApp()
    {
          //OpenAI Client
        string? openaiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (string.IsNullOrWhiteSpace(openaiApiKey))
        {
            throw new Exception("OPENAI_API_KEY is not set. Please set the OPENAI_API_KEY environment variable before running the app.");
        }

        _embeddingClient = new EmbeddingClient("text-embedding-3-small", openaiApiKey);
        _chatClient = new ChatClient(model: "gpt-5.4-mini", openaiApiKey);

        //MongoDB Client
        string mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") 
                                       ?? "mongodb://localhost:27017";
        
        if (string.IsNullOrEmpty(mongoConnectionString))
        {
            throw new Exception("MONGO_URI is not set.");
        }

    
        var mongoClient = new MongoClient(mongoConnectionString); 
        var database = mongoClient.GetDatabase("BakeryDb");
        
        _bakeryCollection = database.GetCollection<BsonDocument>("BakeryDataEmbeddings");
    }
    public async Task<bool> AddbakerydataAsync()
    {
        try
        {
            string bakeryData = """
                Bakery Name: Sweet Crumbs Bakery
                Sweet Crumbs Bakery is a family-owned artisan bakery established in 2018. We specialize in freshly baked breads, pastries, cakes, cookies, and handcrafted beverages. All products are baked daily using high-quality ingredients.

                ___

                Bakery Address

                Sweet Crumbs Bakery
                24 Maple Street
                Greenwood District
                Springfield, IL 62704
                United States

                Nearby Landmarks:
                - Greenwood Public Library (2-minute walk)
                - Central Park (5-minute walk)
                - Springfield Mall (10-minute walk)

                Parking:
                Free customer parking is available behind the bakery.

                ___

                Contact Information

                Phone: +1 (217) 555-0148

                WhatsApp: +1 (217) 555-0199

                Email: hello@sweetcrumbsbakery.com

                Website: www.sweetcrumbsbakery.com

                Instagram: @sweetcrumbsbakery

                Facebook: Sweet Crumbs Bakery

                ___

                Business Hours

                Monday: 7:00 AM – 7:00 PM

                Tuesday: 7:00 AM – 7:00 PM

                Wednesday: 7:00 AM – 7:00 PM

                Thursday: 7:00 AM – 8:00 PM

                Friday: 7:00 AM – 8:00 PM

                Saturday: 8:00 AM – 8:00 PM

                Sunday: 8:00 AM – 5:00 PM

                Public holidays may have different operating hours.

                ___

                Bread Menu

                Classic Sourdough Loaf - $6.50

                Whole Wheat Bread - $5.75

                French Baguette - $3.50

                Multigrain Bread - $6.25

                Garlic Herb Bread - $5.95

                Brioche Loaf - $7.25

                Cheese Stuffed Bread - $8.50

                All breads are baked fresh every morning.

                ___

                Pastries

                Butter Croissant - $3.25

                Chocolate Croissant - $3.95

                Almond Croissant - $4.50

                Cinnamon Roll - $4.25

                Apple Danish - $4.15

                Blueberry Danish - $4.20

                Cream Cheese Danish - $4.40

                Spinach & Cheese Puff - $5.25

                ___

                Cakes

                Chocolate Truffle Cake (1 kg) - $38.00

                Black Forest Cake (1 kg) - $36.00

                Red Velvet Cake (1 kg) - $40.00

                Vanilla Celebration Cake (1 kg) - $34.00

                Carrot Walnut Cake (1 kg) - $35.00

                Cheesecake (Whole) - $42.00

                Custom birthday cakes require at least 48 hours' notice.

                ___

                Cookies

                Chocolate Chip Cookie - $2.25

                Double Chocolate Cookie - $2.50

                Oatmeal Raisin Cookie - $2.25

                Peanut Butter Cookie - $2.40

                White Chocolate Macadamia Cookie - $2.75

                Sugar Cookie - $2.00

                Cookies are available individually or by the dozen.

                ___

                Beverages

                Espresso - $2.75

                Americano - $3.00

                Cappuccino - $4.25

                Latte - $4.50

                Mocha - $4.95

                Hot Chocolate - $4.25

                English Breakfast Tea - $3.00

                Green Tea - $3.00

                Fresh Orange Juice - $4.50

                Iced Coffee - $4.75

                ___

                Seasonal Specials

                Pumpkin Spice Latte (Autumn) - $5.50

                Strawberry Shortcake (Summer) - $39.00

                Christmas Fruit Cake (December) - $45.00

                Mango Cheesecake (Summer) - $43.00

                Valentine Heart Cake (February) - $42.00

                Seasonal products are available only during their respective seasons.

                ___

                Special Orders

                Customers may place custom cake orders for birthdays, weddings, anniversaries, graduations, and corporate events.

                Minimum notice:
                - Standard custom cakes: 48 hours
                - Wedding cakes: 7 days

                Edible photo printing is available for an additional $15.

                ___

                Dietary Options

                Available upon request:

                - Gluten-Free Bread
                - Vegan Chocolate Cake
                - Eggless Cakes
                - Sugar-Free Cheesecake
                - Dairy-Free Cookies

                These items require advance ordering.

                ___

                Delivery Information

                Delivery Radius: 15 miles

                Delivery Fee:
                0–5 miles: Free
                5–10 miles: $5
                10–15 miles: $8

                Orders above $75 receive free delivery.

                Delivery hours:
                9:00 AM – 6:00 PM

                ___

                Payment Methods

                Accepted payment methods:

                - Cash
                - Visa
                - Mastercard
                - American Express
                - Apple Pay
                - Google Pay
                - PayPal

                Gift cards are also accepted.

                ___

                Refund Policy

                Products damaged during delivery will be replaced or refunded.

                Customized cakes cannot be refunded after preparation has begun.

                Refund requests must be made within 24 hours of purchase.

                ___

                Loyalty Program

                Customers earn 1 point for every $1 spent.

                Rewards:

                100 points = Free Coffee

                250 points = Free Pastry

                500 points = $25 Bakery Voucher

                Members receive exclusive discounts during holidays.

                ___

                Frequently Asked Questions

                Q: Do you bake fresh every day?
                A: Yes. All breads and pastries are baked fresh daily.

                Q: Do you offer eggless cakes?
                A: Yes. Eggless cakes are available with advance notice.

                Q: Can I customize birthday cakes?
                A: Yes. Custom decorations, flavors, and messages are available.

                Q: Do you deliver?
                A: Yes. Delivery is available within a 15-mile radius.

                Q: Do you offer gluten-free products?
                A: Yes. Selected products are available upon request.

                ___

                Bakery Policies

                Outside food is not permitted inside the bakery.

                Pets are not allowed except certified service animals.

                Wi-Fi is free for all customers.

                Customers are encouraged to place large orders in advance to ensure availability.

                ___

                About the Bakery

                Sweet Crumbs Bakery believes in handcrafted baking using locally sourced ingredients whenever possible. Our mission is to provide fresh, delicious baked goods in a warm and welcoming environment. Every loaf, pastry, and cake is made with care by our experienced bakers, ensuring quality and consistency in every bite.
                """;

            string [] splittedChunks= bakeryData.Split("___", StringSplitOptions.None);
        
            for(int i = 0; i < splittedChunks.Length; i++)
                {
                    string currentChunk=splittedChunks[i].Trim();
                    if (string.IsNullOrEmpty(currentChunk))
                    {
                        continue;
                    }
                    var response=await _embeddingClient.GenerateEmbeddingAsync(currentChunk);

                    OpenAIEmbedding embedding = response.Value;

                    float[] queryVector = embedding.ToFloats().ToArray();

                    if (queryVector.Length == 0)
                    {
                        Console.WriteLine($"Skipping chunk [{i}] - embedding generation failed (empty vector). Embedding object info:");
                    }

                    //storing embeddings
                    var document=new BsonDocument
                    {
                        { "chunkIndex", i },
                        { "text", currentChunk },
                        { "embedding", new BsonArray(queryVector) }, 
                        { "createdAt", DateTime.UtcNow } 
                    };
                        await _bakeryCollection.InsertOneAsync(document); 
                    
                    Console.WriteLine($"Successfully inserted chunk [{i}] into MongoDB.");
                    }
            return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return false;  
            }
        }

    public async Task<List<string>> Retrive(string query)
    {
        var matchedTextResults = new List<string>();

        try
        {
            var response = await _embeddingClient.GenerateEmbeddingAsync(query);
            
            OpenAIEmbedding embedding = response.Value;

            float[] queryVector = embedding.ToFloats().ToArray();

            if (queryVector.Length == 0)
            {
                Console.WriteLine("Query embedding generation failed (empty vector). Skipping retrieval.");
                return matchedTextResults;
            }

            var vectorSearchStage = new BsonDocument("$vectorSearch", new BsonDocument
            {
                {"index","vector_index"},
                {"path","embedding"},
                {"queryVector",new  BsonArray(queryVector)},
                {"numCandidates",100},
                {"limit",4}
            });

            var pipeline=new[] {vectorSearchStage};
            
            using(var cursor=await _bakeryCollection.AggregateAsync<BsonDocument>(pipeline))
            {
                 while (await cursor.MoveNextAsync()) 
            {
                foreach (var doc in cursor.Current)
                {
                    if (doc.Contains("text"))
                    {
                        matchedTextResults.Add(doc["text"].AsString);
                    }
                }
            
            }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during retrieval: {ex.Message}");
        }
        List <String>  Context= [string.Join(" | ", matchedTextResults)];

        return Context;
    }

    public async Task<string> Augmentation(string query,List<string> context)
    {
        string joinedContext = string.Join("\n\n", context);
        string prompt=$""" 
        you are an AI assistant tasked to help user to answer their query about the backery- Sweet Crumbs Bakery 
        so your task is to give answer that cover all points in the query of user using the provided context only.
        so you are only suppose to answer the user's query only using the provided context.you are not supposed to answer without it.
        make sure your answer is relevent to users  query.
        if the provided context doesnt have information to answer user's query then simply state that 'Sorry,I dont have the information to answer your query'
        you are only suppose to answer queries that is related to the bakery and its services and its products.if any query is not related to 
        this topics that relates to bakery then clearly state that your task is to help you on anything related to Sweet Crumbs Bakery do you have any query on that then please let me know ,i can help you with that
        input:
        <query>
        {query}
        </query>
        
        <context>
        {joinedContext}
        </context>
        """;

        try
        {
            var payload=new List<ChatMessage>
            {
                new UserChatMessage(prompt)
            };
            ChatCompletion completion = await _chatClient.CompleteChatAsync(payload);

            var content = completion?.Content;

            if (completion?.Content != null && completion.Content.Count > 0)
            {
                return completion.Content[0].Text ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error during generating answer: {ex.Message}");
            return string.Empty;
        }

        return string.Empty;
    }

    public async Task<string> ChatFunction(string query)
    {
        var data = await _bakeryCollection.Find("{}").FirstOrDefaultAsync();

        if (data == null)
        {
         await  AddbakerydataAsync();
        }
        var retrivedChunks=await Retrive(query:query);
        var answer=await Augmentation(context:retrivedChunks,query:query);
        return answer;

    }

}


