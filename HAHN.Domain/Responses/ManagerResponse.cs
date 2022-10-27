namespace HAHN.Domain.Responses
{
    public class ErrorObject
    {
        public string? Text { get; set; }
        public ResponseErrorCodes Code { get; set; }
    }
    public interface IManagerResponseBase
    {
        List<string> Errors { get; set; }

        List<ErrorObject> ErrorObjects { get; set; }
        bool Success { get; }
        string SuccessMessage { get; set; }

        List<string> SuccessMessages { get; set; }

        int Pages { get; set; }

        int Page { get; set; }

        int RecordsCount { get; set; }



        string ErrorsMessage();
    }
    public interface IManagerResponse<T>
    {

        T Data { get; }

    }
    public class ManagerResponse<T> : IManagerResponse<T>, IManagerResponseBase
    {
        public bool Unauthorized { get; set; }


        public ManagerResponse()
        {
            Errors = new List<string>();
            SuccessMessages = new List<string>();
            ErrorObjects = new List<ErrorObject>();
        }
        public List<string> Errors { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public int RecordsCount { get; set; }

        public bool ServerException { get; set; }

        public List<ErrorObject> ErrorObjects { get; set; }

        public List<string> SuccessMessages { get; set; }

        public string SuccessMessage
        {
            get
            {
                if (SuccessMessages.Count == 1)
                {
                    return SuccessMessages[0];
                }
                else
                {
                    return String.Join("\r\n", SuccessMessages.ToArray());
                }
            }
            set
            {
                if (!SuccessMessages.Any(x => x.ToLower() == value.ToLower()))
                {
                    SuccessMessages.Add(value);
                }

            }
        }

        public string ErrorsMessage()
        {
            var ret = "";
            foreach (var er in Errors)
            {
                ret += er + "\r\n";
            }
            return ret;
        }

        private T data;
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public ManagerResponse<T> Add(List<string> items, ResponseErrorCodes errorCode)
        {
            foreach (var item in items)
            {
                Add(item, errorCode);
            }

            return this;
        }

        public ManagerResponse<T> Add(List<string> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }

            return this;
        }

        public ManagerResponse<T> Add(string item, ResponseErrorCodes errorCode)
        {
            if (!Errors.Any(x => x.ToLower() == item.ToLower()))
            {
                Errors.Add(item);
            }

            if (!ErrorObjects.Any(x => x.Text.ToLower() == item.ToLower()))
            {
                ErrorObjects.Add(new ErrorObject() { Text = item, Code = errorCode });
            }
            return this;
        }

        public ManagerResponse<T> Add(string item)
        {
            if (!Errors.Any(x => x.ToLower() == item.ToLower()))
            {
                Errors.Add(item);
            }
            return this;
        }

        public ManagerResponse<T> Add(IEnumerable<string> items)
        {
            Errors.AddRange(items.ToList());
            return this;
        }

        public ManagerResponse<T> Add(IManagerResponseBase resp)
        {
            Errors.AddRange(resp.Errors);
            ErrorObjects.AddRange(resp.ErrorObjects);
            Page = resp.Page;
            Pages = resp.Pages;
            SuccessMessages.AddRange(resp.SuccessMessages);
            return this;
        }

        public ManagerResponse<T> SetData(T obj)
        {
            Data = obj;
            return this;
        }

        public bool Success
        {
            get
            {
                return Errors.Count == 0 && !ServerException && !Unauthorized;
            }

        }
    }

    public enum ResponseErrorCodes : int
    {
        InvalidAccess = 100,
        ExternalIdNotUnique = 143,
        ClientIdentifierRequired = 144,
        ResourceNotFound = 145,
        PasswordMatchError = 146,
    }
}
