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
    public class TagRepository : BaseRepository
    {
        public TagRepository(IConfiguration config) : base(config) { }
        public List<Tag> GetAllTags()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id,
                              Name
                         FROM Tag
                         ORDER BY Name";
                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(NewTagFromReader(reader));
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        //public Post GetPublisedPostById(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //               SELECT p.Id, p.Title, p.Content, 
        //                      p.ImageLocation AS HeaderImage,
        //                      p.CreateDateTime, p.PublishDateTime, p.IsApproved,
        //                      p.CategoryId, p.UserProfileId,
        //                      c.[Name] AS CategoryName,
        //                      u.FirstName, u.LastName, u.DisplayName, 
        //                      u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
        //                      u.UserTypeId, 
        //                      ut.[Name] AS UserTypeName
        //                 FROM Post p
        //                      LEFT JOIN Category c ON p.CategoryId = c.id
        //                      LEFT JOIN UserProfile u ON p.UserProfileId = u.id
        //                      LEFT JOIN UserType ut ON u.UserTypeId = ut.id
        //                WHERE IsApproved = 1 AND PublishDateTime < SYSDATETIME()
        //                      AND p.id = @id";

        //            cmd.Parameters.AddWithValue("@id", id);
        //            var reader = cmd.ExecuteReader();

        //            Post post = null;

        //            if (reader.Read())
        //            {
        //                post = NewTagFromReader(reader);
        //            }

        //            reader.Close();

        //            return post;
        //        }
        //    }
        //}

        //public Post GetUserPostById(int id, int userProfileId)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //               SELECT p.Id, p.Title, p.Content, 
        //                      p.ImageLocation AS HeaderImage,
        //                      p.CreateDateTime, p.PublishDateTime, p.IsApproved,
        //                      p.CategoryId, p.UserProfileId,
        //                      c.[Name] AS CategoryName,
        //                      u.FirstName, u.LastName, u.DisplayName, 
        //                      u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
        //                      u.UserTypeId, 
        //                      ut.[Name] AS UserTypeName
        //                 FROM Post p
        //                      LEFT JOIN Category c ON p.CategoryId = c.id
        //                      LEFT JOIN UserProfile u ON p.UserProfileId = u.id
        //                      LEFT JOIN UserType ut ON u.UserTypeId = ut.id
        //                WHERE p.id = @id AND p.UserProfileId = @userProfileId";

        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.Parameters.AddWithValue("@userProfileId", userProfileId);
        //            var reader = cmd.ExecuteReader();

        //            Post post = null;

        //            if (reader.Read())
        //            {
        //                post = NewPostFromReader(reader);
        //            }

        //            reader.Close();

        //            return post;
        //        }
        //    }
        //}


        public void AddTag(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        insert into Tag (
                            Name)
                        output inserted.id
                        values (
                            @name )";
                    cmd.Parameters.AddWithValue("@name", tag.Name);
                   

                    tag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        private Tag NewTagFromReader(SqlDataReader reader)
        {
            return new Tag()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
               
                
            };
        }

        //public void DeletePost(int postId)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                    DELETE FROM Post
        //                    WHERE Id = @id";

        //            cmd.Parameters.AddWithValue("@id", postId);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public void UpdatePost(Post post)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                       UPDATE Post
        //                       SET
        //                            Title = @Title,
        //                            Content = @Content,
        //                            ImageLocation = @ImageLocation,
        //                            PublishDateTime = @PublishDateTime,
        //                            CategoryId = @CategoryId
        //                        WHERE Id = @id";

        //            cmd.Parameters.AddWithValue("@Title", post.Title);
        //            cmd.Parameters.AddWithValue("@Content", post.Content);
        //            cmd.Parameters.AddWithValue("@CategoryId", post.CategoryId);
        //            cmd.Parameters.AddWithValue("@id", post.Id);

        //            if (post.PublishDateTime == null)
        //            {
        //                cmd.Parameters.AddWithValue("@PublishDateTime", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@PublishDateTime", post.PublishDateTime);
        //            }

        //            if (post.ImageLocation == null)
        //            {
        //                cmd.Parameters.AddWithValue("@ImageLocation", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@ImageLocation", post.ImageLocation);
        //            }

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
