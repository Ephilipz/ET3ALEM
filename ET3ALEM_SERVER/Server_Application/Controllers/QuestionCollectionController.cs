﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataServiceLayer;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server_Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCollectionController : ControllerBase
    {
        private readonly IQuestionCollectionDsl _iQuestionCollectionDsl;

        public QuestionCollectionController(IQuestionCollectionDsl questionCollectionDsl)
        {
            _iQuestionCollectionDsl = questionCollectionDsl;
        }

        // GET api/<QuestionCollectionController>
        [HttpGet]
        public async Task<ActionResult<List<QuestionCollection>>> GetQuestionCollections()
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _iQuestionCollectionDsl.GetQuestionCollections(userId);
        }

        // GET api/<QuestionCollectionController>/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionCollection>> GetQuestionCollection(int id)
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _iQuestionCollectionDsl.GetQuestionCollection(id, userId);
        }

        // POST api/<QuestionCollectionController>
        [HttpPost]
        public async Task<ActionResult<QuestionCollection>> Post(QuestionCollection questionCollection)
        {
            var userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            questionCollection.UserId = userId;
            return await _iQuestionCollectionDsl.InsertQuestionCollection(questionCollection);
        }

        [HttpDelete]
        public async Task<ActionResult<QuestionCollection>> DeleteQuestionCollection(int id)
        {
            return await _iQuestionCollectionDsl.DeleteQuestionCollection(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuestionCollection questionCollection)
        {
            if (id != questionCollection.Id)
                return BadRequest();
            await _iQuestionCollectionDsl.PutQuestionCollection(id, questionCollection);
            return NoContent();
        }

        // GET api/<QuestionCollectionController>/IsNameUnique/:name
        [HttpGet("NameExists/{name}")]
        public async Task<ActionResult<bool>> NameExists(string name)
        {
            return await _iQuestionCollectionDsl.NameExists(name);
        }
    }
}