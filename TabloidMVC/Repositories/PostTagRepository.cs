using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class PostTagRepository : BaseRepository
    {
        public PostTagRepository(IConfiguration config) : base(config) { }




        public List<PostTag> GetAllPostTags()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, PostId, TagId
                         FROM PostTag";
                    var reader = cmd.ExecuteReader();

                    var postTags = new List<PostTag>();

                    while (reader.Read())
                    {
                        postTags.Add(NewPostTagFromReader(reader));
                    }

                    reader.Close();

                    return postTags;
                }
            }
        }


        private PostTag NewPostTagFromReader(SqlDataReader reader)
        {
            return new PostTag()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                TagId = reader.GetInt32(reader.GetOrdinal("TagId"))
            };
        }


        public void Add(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO PostTag (
                            PostId, TagId)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @postId, @tagId )";
                    cmd.Parameters.AddWithValue("@postId", postTag.PostId);
                    cmd.Parameters.AddWithValue("@tagId", postTag.TagId);
                    

                    postTag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeletePostTag(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM PostTag
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public PostTag GetPostTagByPostIdAndTagId(int postId, int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, PostId, TagId
                         FROM PostTag
                            
                        WHERE PostId = @postId AND TagId = @tagId";

                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    var reader = cmd.ExecuteReader();

                    PostTag postTag = null;

                    if (reader.Read())
                    {
                        postTag = NewPostTagFromReader(reader);
                    }

                    reader.Close();

                    return postTag;
                }
            }
        }




    }
}
