using RestSharp;
using System.Threading.Tasks;

namespace Read_and_learn.Service.Translation.ReversoApi
{
    /// <summary>
    /// Handle request to the reverso.net API.
    /// </summary>
    internal class ReversoApi
    {
        /// <summary>
        /// Target API URL.
        /// </summary>
        public const string Url = "https://cps.reverso.net/api2";

        private readonly RestClient _client;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReversoApi()
        {
            _client = new RestClient(Url);
        }

        /// <summary>
        /// Send post request via target <paramref name="url"/> and <paramref name="model"/>.
        /// </summary>
        /// <typeparam name="T2">Target result type</typeparam>
        /// <param name="url">Target url</param>
        /// <param name="model">Target model</param>
        /// <returns>
        ///     <see cref="IRestResponse{T2}"/>.
        /// </returns>
        public async Task<IRestResponse<T2>> SendPostRequest<T2>(string url, object model)
        {
            var request = new RestRequest(url) { Method = Method.POST };
            request.AddJsonBody(model);

            var result = await _client.ExecutePostAsync<T2>(request);

            return result;
        }
    }
}
