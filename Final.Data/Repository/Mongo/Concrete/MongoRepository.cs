using System;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Context;
using Final.Data.Repository.Mongo.Abstarct;
using MongoDB.Driver;

namespace Final.Data.Repository.Mongo.Concrete
{
	public class MongoRepository<TEntity, TDto> : IMongoRepository<TEntity>
        where TEntity : class, IEntity
        where TDto : class, IEntity
	{
        //mongoDB bağlantıları tanımlıyoruz.
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IMongoCollection<TEntity> collection)
        {
            //burada kısaca bağlantımızı gerçekleştiriyoruz. 
            //bunun için zaten biz DataBaseSettings içinde zaten biz mongoDB ayarları tanımlıyoruz oradan kısaca alıyoruz.
            //var client = new MongoClient(databaseSettings.ConnectionString);
            //var database = client.GetDatabase(databaseSettings.DatabaseName);

            //databaseSettings.AgreeListCollectionName
            //_collection = database.GetCollection<TEntity>(databaseSettings.AgreeListCollectionName);
            _collection = collection;
        }

        public async Task<BaseResponse<List<TEntity>>> GetAllAsync()
        {
            //bütün categoirleri MOngoDb Den aldık.
            var response = await _collection.Find(_ => true).ToListAsync();

            return BaseResponse<List<TEntity>>.Success(response, 200);
        }

        public async Task<BaseResponse<TEntity>> CreateAsync(TEntity creatDto)
        {
            //var category = _mapper.Map<Category>(categoryDto);
            //direk luşturduk.(geriye dönmüyor)
            await _collection.InsertOneAsync(creatDto);

            //return BaseResponse<TDto>.Success(_mapper.Map<CategoryDto>(category), 200);
            return BaseResponse<TEntity>.Success(creatDto, 201);
        }
        /*
        public async Task<BaseResponse<TDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                //eğer yoksa.
                return BaseResponse<TDto>.Fail("Category Not Found", 404);
            }

            return BaseResponse<TDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        */
    }
}

