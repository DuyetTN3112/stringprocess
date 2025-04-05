using System;
using System.Collections.Generic;
using StringProcessingApp.Interfaces;
using StringProcessingApp.Models;

namespace StringProcessingApp.Services
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _messageRepository.GetAllMessages();
        }

        public IEnumerable<Message> GetMessagesByuser_id(int user_id)
        {
            return _messageRepository.GetMessagesByuser_id(user_id);
        }

        public IEnumerable<Message> GetMessagesSortedAlphabetically(bool ascending)
        {
            return _messageRepository.GetMessagesSortedAlphabetically(ascending);
        }

        public IEnumerable<Message> GetMessagesSortedByDate(bool ascending)
        {
            return _messageRepository.GetMessagesSortedByDate(ascending);
        }

        public IEnumerable<Message> SearchMessages(string searchTerm)
        {
            return _messageRepository.SearchMessages(searchTerm);
        }

        public void SaveEncodedMessage(User user, string inputString, int shiftValue)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var stringProcessor = new StringProcessing(inputString, shiftValue);

            var message = new Message
            {
                user_id = user.id,
                message_text = inputString,
                encoded_text = stringProcessor.Encode(),
                shift_value = shiftValue, // Added this line
                input_codes = stringProcessor.GetInputCodesAsString(),  // Thêm dòng này
                output_codes = stringProcessor.GetOutputCodesAsString() // Thêm dòng này
            };

            _messageRepository.AddMessage(message);
        }
    }
}