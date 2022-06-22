using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ProductContracts
    {
        public const string GetAllProductsQuery = @"
        query {
            products {
            totalCount
            items {
                id
                attributeIdValue {
                id, value
                }
                name,
                category {
                id, name, properties {
                    id, name
                }
                }
                price
                rating
                image {
                id
                imagePath
                }
                reviews {
                id, user {
                    id
                } content, rating, date
                }, description
            }
            }
        }";
    }   
}
