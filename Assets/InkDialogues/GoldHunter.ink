INCLUDE globals.ink

->start

= start
    Hello! # Speaker: Gold Hunter
        * {GoldHunter == 0} Hi! # Speaker: You
        Get me 2 pieces of gold yo! # Speaker: Gold Hunter
            **Fine. #Speaker: You #Accept: GoldHunter
            ~ GoldHunter = 1
            ->END
        * {GoldHunter == 1} Hi! # Speaker: You
        Did you do what I asked you? # Speaker: Gold Hunter
            ** Not yet.. # Speaker: You
            ->END
        * {GoldHunter == 2} I've done it! # Speaker: You
        Great! here's your reward! # Speaker: Gold Hunter # Complete: GoldHunter
            ~ GoldHunter = 3
            ->END
        * {GoldHunter == 3} Hi! # Speaker: You
        Sorry, got nothing for you mate. # Speaker: Gold Hunter
            ->END
        * Bye. # Speaker: You
        ->END
        