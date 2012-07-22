-- ThinkManage.Category
CREATE TABLE [category] (
    [p_id] int NOT NULL,                    -- _pId
    [id2] int NULL,                         -- _id2
    [id] int IDENTITY NOT NULL,             -- _id
    [date_created] datetime NOT NULL,       -- _dateCreated
    [category_name] varchar(255) NULL,      -- _categoryName
    CONSTRAINT [pk_category] PRIMARY KEY ([id])
)
go

-- ThinkManage.Tag
CREATE TABLE [tag] (
    [tag_name] varchar(255) NULL,           -- _tagName
    [id] int IDENTITY NOT NULL,             -- _id
    [date_created] datetime NOT NULL,       -- _dateCreated
    CONSTRAINT [pk_tag] PRIMARY KEY ([id])
)
go

-- ThinkManage.ThinkContents
CREATE TABLE [think_contents] (
    [think_content] varchar(255) NULL,      -- _thinkContent
    [tag] int NOT NULL,                     -- _tag
    [id] int IDENTITY NOT NULL,             -- _id
    [date_created] datetime NOT NULL,       -- _dateCreated
    [category_id] int NOT NULL,             -- _category
    CONSTRAINT [pk_think_contents] PRIMARY KEY ([id])
)
go

CREATE INDEX [idx_category_id2] ON [category]([id2])
go

CREATE INDEX [idx_think_contents_category_id] ON [think_contents]([category_id])
go

CREATE INDEX [idx_think_contents_tag] ON [think_contents]([tag])
go

ALTER TABLE [think_contents] ADD CONSTRAINT [ref_think_contents_category] FOREIGN KEY ([category_id]) REFERENCES [category]([id])
go

ALTER TABLE [think_contents] ADD CONSTRAINT [ref_think_contents_tag] FOREIGN KEY ([tag]) REFERENCES [tag]([id])
go

