using System;
using System.Text.Json.Serialization;

namespace Final.Base.Response
{
	public class BaseResponse<T>
	{
        //biz burada serverdan gelen istekler client tarafına sadece bu Dto ile dönmesini istiyoruz.

        //başarılı olması durumunda verileri bunun içine atarız.
        public T Data { get; set; }

        //burada zaten status code bize otomatik dönüyor biz burada response içinde olma dedik ama kayıtlı ol.
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        //başarısız olduğunda hataları alıcak:
        public List<string> Errors { get; set; }

        //Static Factory Methodur.
        public static BaseResponse<T> Success(T data, int statusCode)
        {
            return new BaseResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static BaseResponse<T> Success(int statusCode)
        {
            return new BaseResponse<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = false };
        }

        //çoklu hata varsa.
        public static BaseResponse<T> Fail(List<string> errors, int statusCode)
        {
            return new BaseResponse<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
        //tek bir hata varsa buraya yazalım.
        public static BaseResponse<T> Fail(string error, int statusCode)
        {
            return new BaseResponse<T>
            {
                Errors = new List<string> { error },
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
    }
}

