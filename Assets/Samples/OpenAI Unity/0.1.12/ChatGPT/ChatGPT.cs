using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        const string API_KEY = "";
        const string ORGANIZATION = "";
        //[SerializeField] private InputField inputField;
        //[SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        //private OpenAIApi openai = new OpenAIApi();
        private OpenAIApi openai = new OpenAIApi(API_KEY, ORGANIZATION);

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";

        private void Start()
        {
            //button.onClick.AddListener(SendReply);

        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        public async Task<string> SendReply(string content)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                //Content = inputField.text
                Content = content
            };

            AppendMessage(newMessage);

            //if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text;
            if (messages.Count == 0) newMessage.Content = prompt + "\n" + content;

            messages.Add(newMessage);

            //button.enabled = false;
            //inputField.text = "";
            //inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                messages.Add(message);
                AppendMessage(message);
                return message.Content;
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
                return null;
            }

            //button.enabled = true;
            //inputField.enabled = true;
        }
    }
}
