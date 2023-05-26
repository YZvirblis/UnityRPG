INCLUDE globals.ink

->start

= start
    Hello! # Speaker: Quest Giver
        * {ShadyMonsters == 0} Hi! # Speaker: You
        Go kill that monster yo! # Speaker: Quest Giver
            **Fine. #Speaker: You #Accept: ShadyMonsters
            ~ ShadyMonsters = 1
            ->END
        * {ShadyMonsters == 1} Hi! # Speaker: You
        Did you do what I asked you? # Speaker: Quest Giver
            ** Not yet.. # Speaker: You
            ->END
        * {ShadyMonsters == 2} I've done it! # Speaker: You
        Great! here's your reward! # Speaker: Quest Giver # Complete: ShadyMonsters
            ~ ShadyMonsters = 3
            ->END
        * {ShadyMonsters == 3} Hi! # Speaker: You
        Sorry, got nothing for you mate. # Speaker: Quest Giver
            ->END
        * Bye. # Speaker: You
        ->END
        