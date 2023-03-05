using System;
using System.Collections.Generic;
using AutoMapper;
using Azure;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Context;
using Final.Data.Model.DatabaseMongo;
using Final.Data.Repository.Mongo.Abstarct;
using Final.Dto.Dtos;
using MongoDB.Driver;

namespace Final.Data.Repository.Mongo.Concrete
{
	public class AgreeListRepository : IAgreeListRepository
    {
        private readonly IMongoCollection<AgreeList> _listCollection;
        private readonly IMapper _mapper;

        public AgreeListRepository(IMapper mapper, IMongoDbSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _listCollection = database.GetCollection<AgreeList>(databaseSettings.AgreeListCollectionName);
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<AgreeList>>> GetAllAsync()
        {
            //bütün Listeleri MOngoDb Den aldık.
            var lists = await _listCollection.Find(_ => true).ToListAsync();

            return BaseResponse<List<AgreeList>>.Success(lists, 200);
        }

        public async Task<BaseResponse<string>> CreateAsync(AgreeList list)
        {
            //var category = _mapper.Map<Category>(categoryDto);
            //direk luşturduk.(geriye dönmüyor)
            await _listCollection.InsertOneAsync(list);

            return BaseResponse<string>.Success("Created", 201);
        }

        public async Task<BaseResponse<List<AgreeList>>> GetByUserId(int userId)
        {
            var list = await _listCollection.Find<AgreeList>(x => x.UserId == userId).ToListAsync();

            if(list is null)
            {
                return BaseResponse<List<AgreeList>>.Fail("you have no list completed yet", 200);
            }
            return BaseResponse<List<AgreeList>>.Success(list, 200);
        }
    }
}

